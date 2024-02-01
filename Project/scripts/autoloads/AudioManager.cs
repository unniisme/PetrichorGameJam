using Godot;
using System;
using System.Collections.Generic;

namespace Gamelogic.Audio
{
    public partial class AudioManager : Node2D
    {
        public static readonly Dictionary<StringName, AudioStreamPlayer> audioStreams = new();

        public override void _Ready()
        {
            foreach (Node child in GetChildren())
            {
                if (child is AudioStreamPlayer stream)
                {
                    audioStreams[stream.Name] = stream;
                }
            }
            PlayStream("happyBackground");
        }

        /// <summary>
        /// Get a certain audio stream player
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AudioStreamPlayer GetStreamPlayer(StringName name)
        {
            return audioStreams[name];
        }


        /// <summary>
        /// Play a certain audio stream player
        /// </summary>
        /// <param name="name"></param>
        public static void PlayStream(StringName name)
        {
            audioStreams[name].Play();
        }
        public static void StopStream(StringName name)
        {
            audioStreams[name].Stop();
        }
    }
}
