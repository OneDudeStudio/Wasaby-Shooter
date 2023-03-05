using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace BeatDetection
{
    public struct DetectedBeat
    {
        public TimeSpan TimeOffset;
        public double StrongestFrequency;
    }

    public class BeatDetector1
    {
        double[] FourierScore; // B
        double[] CurrentWindowEnergy; // Es
        double[][] HistoricEnergy; // Ei
        double[] AverageEnergy; // <Ei>
        Complex[] fftData;
        double[] EnergyVariance; // V
        int[] bandWidths; // w
        const double C = 250;
        const double V0 = 150;
        const int windowSize = 1024; // has to be power of 2
        const int stepSize = 256; // 0.25 * windows size
        const int bandCount = 32; // between 20 and 64
        const int historySize = 43; // ~0.56 sec because 43 * 1024 ~ 1 sec

        // bandWidths[i] = a * (i+1) + b
        // such that sum of bandWidths = windowSize
        const double bandWb = (4160.0 - 1024.0) / 2016.0;
        const double bandWa = 2.0 - bandWb;

        List<DetectedBeat> beats = new List<DetectedBeat>();
        int sampleRate;

        // input data
        float[] leftChannel;
        float[] rightChannel;

        public BeatDetector1(int sampleRate)
        {
            this.sampleRate = sampleRate;

            FourierScore = new double[windowSize];
            CurrentWindowEnergy = new double[bandCount];
            HistoricEnergy = new double[bandCount][];
            for (int i = 0; i < bandCount; i++) HistoricEnergy[i] = new double[historySize];
            AverageEnergy = new double[bandCount];
            EnergyVariance = new double[bandCount];

            fftData = new Complex[windowSize];
            bandWidths = new int[bandCount];
            for (int i = 0; i < bandCount; i++) bandWidths[i] = (int)Math.Round(bandWa * (i + 1) + bandWb);
            Debug.Assert(bandWidths.Sum() == 1024, $"{bandWidths.Sum()} != 1024");
        }

        public void DetectSegment(Complex[] buffer)
        {
            //// TODO: rewrite this into stackallocked windows like V2
            //leftChannel = new float[memory.Length / 2];
            //rightChannel = new float[memory.Length / 2];

            //// copy pcm data into left/right channel arrays
            //for (int i = 0; i < memory.Length; i += 2)
            //{
            //    leftChannel[i / 2] = memory[i];
            //    rightChannel[i / 2] = memory[i + 1];
            //}

            //for (int offset = 0; offset < leftChannel.Length - windowSize; offset += stepSize)
            //{
            //    float[] windowL = new float[windowSize];
            //    float[] windowR = new float[windowSize];
            //    Array.Copy(leftChannel, offset, windowL, 0, windowSize);
            //    Array.Copy(rightChannel, offset, windowR, 0, windowSize);

            //    FillFFTData(windowL, windowR, fftData);

            //    Fourier.FFT(windowSize, fftData);

            fftData = buffer;

            ComputeFourierScore_FFTModulusSquared(fftData);

            ComputeEnergyPerBand();

            ComputeAvgEnergyFromHistory();

            ComputeEnergyVarianceFromHistory();

            UpdateHistory();

            var bandsAbove = CurrentEnergyAboveLocalLimit();

            if (bandsAbove > 0)
            {
                //beats.Add(new DetectedBeat
                //{
                //    TimeOffset = new TimeSpan((offset + windowSize / 2) * (TimeSpan.TicksPerSecond / sampleRate)),
                //    StrongestFrequency = GetStrongestFrequency(),
                //});
            }
        }

        public List<DetectedBeat> GetBeats()
        {
            return beats;
        }

        private double GetStrongestFrequency()
        {
            double m = 0;
            double mIdx = 0;
            for (int i = 0; i < FourierScore.Length; i++)
            {
                if (FourierScore[i] > m)
                {
                    m = FourierScore[i];
                    mIdx = (double)i / (double)FourierScore.Length;
                }
            }

            return mIdx;
        }

        private ulong CurrentEnergyAboveLocalLimit()
        {
            ulong bands = 0;
            ulong bit = 1;
            for (int i = 0; i < bandCount; i++)
            {
                if (CurrentWindowEnergy[i] > C * AverageEnergy[i] && EnergyVariance[i] > V0)
                    bands |= bit;
                bit <<= 1;
            }
            return bands;
        }

        private void UpdateHistory()
        {
            // shift Ei to the right and update
            for (int i = 0; i < bandCount; i++)
            {
                var prev = HistoricEnergy[i];
                HistoricEnergy[i] = new double[historySize];
                Array.Copy(prev, 0, HistoricEnergy[i], 1, historySize - 1);
                HistoricEnergy[i][0] = CurrentWindowEnergy[i];
            }
        }

        private void ComputeEnergyVarianceFromHistory()
        {
            for (int i = 0; i < bandCount; i++)
            {
                double sum = 0;
                for (int k = 0; k < historySize; k++)
                {
                    var delta = HistoricEnergy[i][k] - AverageEnergy[i];
                    sum += delta * delta;
                }
                EnergyVariance[i] = sum / historySize;
            }
        }

        private void ComputeAvgEnergyFromHistory()
        {
            for (int i = 0; i < bandCount; i++)
            {
                double sum = 0;
                for (int k = 0; k < historySize; k++)
                    sum += HistoricEnergy[i][k];
                AverageEnergy[i] = sum / historySize;
            }
        }

        private void ComputeEnergyPerBand()
        {
            var kBase = 0;
            for (int i = 0; i < bandCount; i++)
            {
                double sum = 0;
                for (int k = 0; k < bandWidths[i]; k++)
                    sum += FourierScore[k + kBase];
                CurrentWindowEnergy[i] = bandWidths[i] * (sum / windowSize);

                kBase += bandWidths[i];
            }
        }

        private void ComputeFourierScore_FFTModulusSquared(Complex[] fftData)
        {
            for (int k = 0; k < windowSize; k++)
                FourierScore[k] = fftData[k].Real * fftData[k].Real + fftData[k].Imaginary * fftData[k].Imaginary;
        }

        private static void FillFFTData(float[] windowL, float[] windowR, Complex[] fftData)
        {
            for (int k = 0; k < windowSize; k++)
                fftData[k] = new Complex(windowL[k], windowR[k]);
        }
    }
}