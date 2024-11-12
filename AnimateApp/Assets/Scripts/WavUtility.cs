using System;
using System.IO;
using UnityEngine;

public static class WavUtility
{
    // แปลง AudioClip เป็นข้อมูล WAV
    public static byte[] FromAudioClip(AudioClip clip)
    {
        MemoryStream stream = new MemoryStream();
        const int headerSize = 44;

        // เขียน header
        stream.Seek(0, SeekOrigin.Begin);
        byte[] header = new byte[headerSize];
        stream.Write(header, 0, headerSize);

        // เขียนข้อมูล sample
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        Int16[] intData = new Int16[samples.Length];
        Byte[] bytesData = new Byte[samples.Length * 2];
        int rescaleFactor = 32767; // สำหรับการแปลง float [-1.0f, 1.0f] เป็น Int16

        for (int i = 0; i < samples.Length; i++)
        {
            intData[i] = (short)(samples[i] * rescaleFactor);
            byte[] byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        stream.Write(bytesData, 0, bytesData.Length);

        // เพิ่มข้อมูล header
        stream.Seek(0, SeekOrigin.Begin);
        WriteHeader(stream, clip);

        byte[] result = stream.ToArray();
        stream.Close();
        return result;
    }

    // แปลงข้อมูล WAV เป็น AudioClip
    public static AudioClip ToAudioClip(byte[] data)
    {
        const int headerSize = 44;
        int sampleCount = BitConverter.ToInt32(data, 40) / 2;
        int frequency = BitConverter.ToInt32(data, 24);
        int channels = BitConverter.ToInt16(data, 22);

        AudioClip clip = AudioClip.Create("LoadedClip", sampleCount, channels, frequency, false);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            samples[i] = BitConverter.ToInt16(data, headerSize + i * 2) / 32768.0f;
        }

        clip.SetData(samples, 0);
        return clip;
    }

    private static void WriteHeader(Stream stream, AudioClip clip)
    {
        int hz = clip.frequency;
        int channels = clip.channels;
        int samples = clip.samples;

        stream.Write(System.Text.Encoding.UTF8.GetBytes("RIFF"), 0, 4);
        stream.Write(BitConverter.GetBytes(stream.Length - 8), 0, 4);
        stream.Write(System.Text.Encoding.UTF8.GetBytes("WAVE"), 0, 4);
        stream.Write(System.Text.Encoding.UTF8.GetBytes("fmt "), 0, 4);
        stream.Write(BitConverter.GetBytes(16), 0, 4);
        stream.Write(BitConverter.GetBytes((ushort)1), 0, 2);
        stream.Write(BitConverter.GetBytes(channels), 0, 2);
        stream.Write(BitConverter.GetBytes(hz), 0, 4);
        stream.Write(BitConverter.GetBytes(hz * channels * 2), 0, 4);
        stream.Write(BitConverter.GetBytes((ushort)(channels * 2)), 0, 2);
        stream.Write(BitConverter.GetBytes((ushort)16), 0, 2);
        stream.Write(System.Text.Encoding.UTF8.GetBytes("data"), 0, 4);
        stream.Write(BitConverter.GetBytes(samples * channels * 2), 0, 4);
    }
}