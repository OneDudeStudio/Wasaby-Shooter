using System;
using System.Collections.Generic;

namespace BeatDetection
{
    public interface IBeatDetection
    {
        void Detect();

        List<DetectedBeat> GetBeats();
    }
}