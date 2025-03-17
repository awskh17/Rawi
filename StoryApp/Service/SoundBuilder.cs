using System.Speech.Synthesis;

namespace StoryApp.Service;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
public class SoundBuilder : IDisposable
{
    private static string DAVID_BOT = "Microsoft David Desktop";
    private static string Zira_BOT = "Microsoft Zira Desktop";
    private SpeechSynthesizer _synth = new();

    private SoundBuilder()
    {
        _synth.SetOutputToDefaultAudioDevice();
        _synth.SelectVoice(GetSoundBot());

        _synth.Rate = -1; // Speed (-10 to 10)
        _synth.Volume = 100; // Volume (0 to 100)
    }

    public static SoundBuilder Create()
        => new SoundBuilder();


    private string GetSoundBot(SoundBotEnum soundBot = SoundBotEnum.Zira)
        => soundBot switch
        {
            SoundBotEnum.Zira => Zira_BOT,
            _ => DAVID_BOT
        };


    public void Dispose()
    {
        _synth.Dispose();
    }

    public SoundBuilder SoundBot(SoundBotEnum soundBot)
    {
        _synth.SelectVoice(GetSoundBot(soundBot));
        return this;
    }

    public SoundBuilder Volume(int volume = 100, int rate = -1)
    {
        _synth.Rate = rate;
        _synth.Volume = volume;
        return this;
    }

    public SoundBuilder Speak(string speak)
    {
        Console.WriteLine(speak);
        _synth.Speak(speak);
        return this;
    }
}


public enum SoundBotEnum
{
    Zira, David
}