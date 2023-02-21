using System;
using System.IO;
using System.Net;
using System.Reactive;
using System.Reflection;
using System.Threading;
using Avalonia.Threading;
using HanumanInstitute.MediaPlayer.Avalonia.Bass;
using LibVLCSharp.Shared;
using ManagedBass;
using ManagedBass.Fx;
using ReactiveUI;
using SharpAudio;
using SharpAudio.Codec;
using MediaPlayer = ManagedBass.MediaPlayer;

namespace toneUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to toneUI!";
    public static bool ShouldStop { get; set; }

    ReactiveCommand<Unit, Unit> PlaySoundCommand { get; }
    ReactiveCommand<Unit, Unit> StopSoundCommand { get; set; }

    public MainViewModel()
    {
        Console.Write("xx" + Environment.GetEnvironmentVariable("XDG_SESSION_TYPE"));
        PlaySoundCommand = ReactiveCommand.Create(PlaySound);
        StopSoundCommand = ReactiveCommand.Create(StopSound);
        Console.WriteLine("MainViewModel");
        
    }

    private void Player_PlaybackStopped(int handle, int channel, int data, IntPtr user)
    {
        /*
        // This event should only occur when media ends on its own. Discard when pressing Stop.
        if (_isStopping) { return; }

        Dispatcher.UIThread.InvokeAsync(() =>
        {
            // ReleaseChannel();
            base.OnMediaUnloaded();
            MediaFinished?.Invoke(this, EventArgs.Empty);
        });
        */
    }
    
    private void Player_MediaLoaded()
    {
        /*
        //Debug.WriteLine("MediaLoaded");
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            Status = PlaybackStatus.Playing;
            base.Duration = BassActive ? BassDuration : TimeSpan.Zero;
            base.OnMediaLoaded();
        });   private void Player_MediaLoaded()
    {
        //Debug.WriteLine("MediaLoaded");
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            Status = PlaybackStatus.Playing;
            base.Duration = BassActive ? BassDuration : TimeSpan.Zero;
            base.OnMediaLoaded();
        });
    }
        */
    }

    void VlcSharpPlaySound(string url)
    {
        /*
         * System.DllNotFoundException: Unable to load shared library 'libX11' or one of its dependencies. In order to help diagnose loading problems, consider using a tool like strace. If you're using glibc, consider setting the LD_DEBUG environment variable: 
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libX11.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libX11.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libX11.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/liblibX11.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/liblibX11.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/liblibX11.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libX11: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libX11: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libX11: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/liblibX11: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/liblibX11: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/liblibX11: cannot open shared object file: No such file or directory

   at LibVLCSharp.Shared.Core.Native.XInitThreads()
   at LibVLCSharp.Shared.Core.InitializeDesktop(String libvlcDirectoryPath)
   at LibVLCSharp.Shared.Core.Initialize(String libvlcDirectoryPath)
   at toneUI.ViewModels.MainViewModel.VlcSharpPlaySound(String url) in /home/andreas/projects/toneUI/toneUI/toneUI/ViewModels/MainViewModel.cs:line 73
   at toneUI.ViewModels.MainViewModel.PlaySound() in /home/andreas/projects/toneUI/toneUI/toneUI/ViewModels/MainViewModel.cs:line 224
   at ReactiveUI.ReactiveCommand.<>c__DisplayClass0_0.<Create>b__1(IObserver`1 observer) in /_/src/ReactiveUI/ReactiveCommand/ReactiveCommand.cs:line 105
   at System.Reactive.Linq.QueryLanguage.CreateWithDisposableObservable`1.SubscribeCore(IObserver`1 observer) in /_/Rx.NET/Source/src/System.Reactive/Linq/QueryLanguage.Creation.cs:line 35
   at System.Reactive.ObservableBase`1.Subscribe(IObserver`1 observer) in /_/Rx.NET/Source/src/System.Reactive/ObservableBase.cs:line 58
        
        
        
        
System.DllNotFoundException: Unable to load shared library 'libvlc' or one of its dependencies. In order to help diagnose loading problems, consider using a tool like strace. If you're using glibc, consider setting the LD_DEBUG environment variable: 
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libvlc.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libvlc.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libvlc.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/liblibvlc.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/liblibvlc.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/liblibvlc.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libvlc: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libvlc: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libvlc: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/liblibvlc: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/liblibvlc: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/liblibvlc: cannot open shared object file: No such file or directory

   at LibVLCSharp.Shared.Core.Native.LibVLCVersion()
   at LibVLCSharp.Shared.Core.EnsureVersionsMatch() in /home/andreas/projects/libvlcsharp/src/LibVLCSharp/Shared/Core/Core.cs:line 39
   at LibVLCSharp.Shared.Core.Initialize(String libvlcDirectoryPath) in /home/andreas/projects/libvlcsharp/src/LibVLCSharp/Shared/Core/Core.Desktop.cs:line 42
   at toneUI.ViewModels.MainViewModel.VlcSharpPlaySound(String url) in /home/andreas/projects/toneUI/toneUI/toneUI/ViewModels/MainViewModel.cs:line 100
   at toneUI.ViewModels.MainViewModel.PlaySound() in /home/andreas/projects/toneUI/toneUI/toneUI/ViewModels/MainViewModel.cs:line 252
   at ReactiveUI.ReactiveCommand.<>c__DisplayClass0_0.<Create>b__1(IObserver`1 observer) in /_/src/ReactiveUI/ReactiveCommand/ReactiveCommand.cs:line 105
   at System.Reactive.Linq.QueryLanguage.CreateWithDisposableObservable`1.SubscribeCore(IObserver`1 observer) in /_/Rx.NET/Source/src/System.Reactive/Linq/QueryLanguage.Creation.cs:line 35
   at System.Reactive.ObservableBase`1.Subscribe(IObserver`1 observer) in /_/Rx.NET/Source/src/System.Reactive/ObservableBase.cs:line 58        
         */
        
        /*
        Core.Initialize();

        using var libVLC = new LibVLC(enableDebugLogs: true);
        using var media = new Media(libVLC,
            new Uri(url),
            ":no-video");
        
        using var mediaPlayer = new MediaPlayer();

        mediaPlayer.Play();
        Thread.Sleep(5000);
        */
        
        // var uriString = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3";

        var uri = new Uri(url);
Core.Initialize();
// ImportHelper.Native.XInitThreads();
        using var libvlc = new LibVLCSharp.Shared.LibVLC(enableDebugLogs: true);
        using var media = new LibVLCSharp.Shared.Media(libvlc, uri);
        using var mediaplayer = new LibVLCSharp.Shared.MediaPlayer(media);
        
        mediaplayer.Play();
        Thread.Sleep(5000);
        if (mediaplayer.IsSeekable)
        {
            mediaplayer.SeekTo(TimeSpan.FromSeconds(50));
        }
        // 
    }

    void BassPlaySound(string url)
    {
        // https://github.com/mysteryx93/MediaPlayerUI.NET/blob/master/Avalonia.Bass/BassPlayerHost.cs
        try
        {
            var EffectsFloat = false;
            var useEffects = false;
            var autoPlay = true;
            
            ManagedBass.Bass.GetInfo(out var _deviceInfo); //.Valid();
            
            var flagFloat = EffectsFloat ? BassFlags.Float : 0;
            var _chanIn = ManagedBass.Bass
                .CreateStream(url, Flags: useEffects ? flagFloat | BassFlags.Decode : 0); //.Valid();
            var _chanOut = _chanIn;
            // var _chanInfo = ManagedBass.Bass.ChannelGetInfo(_chanIn);
            // ManagedBass.Bass.ChannelSetSync(_chanIn, SyncFlags.End | SyncFlags.Mixtime, 0, Player_PlaybackStopped);
                // .Valid();

                /*
            if (useEffects)
            {
                // Add mix plugin.
                var _chanMix = BassMix.CreateMixerStream(deviceSampleRate ?? _deviceInfo.SampleRate, _chanInfo.Channels, 
                    BassFlags.MixerEnd | BassFlags.Decode).Valid();
                BassMix.MixerAddChannel(_chanMix, _chanIn, 0 | BassFlags.MixerChanNoRampin | BassFlags.AutoFree);

                // Add tempo plugin.
                _chanOut = BassFx.TempoCreate(_chanMix, 0 | BassFlags.FxFreeSource).Valid();
                AdjustEffects();
                AdjustVolume(volume);
                AdjustTempo(speed, rate, pitch);
            }
            */

            if (autoPlay)
            {
                ManagedBass.Bass.ChannelSetAttribute(_chanOut, ChannelAttribute.Volume, 1);
                ManagedBass.Bass.ChannelPlay(_chanOut); //.Valid();
                Bass.Start();
                // Dispatcher.UIThread.InvokeAsync(() => _posTimer?.Start());
            }

            Player_MediaLoaded();
        }
        catch
        {
            /*
                at ManagedBass.Bass.ChannelGetInfo(Int32 Handle)
   at toneUI.ViewModels.MainViewModel.BassPlaySound(String url) in /home/andreas/projects/toneUI/toneUI/toneUI/ViewModels/MainViewModel.cs:line 81
             
             
          Unable to load shared library 'bass' or one of its dependencies. In order to help diagnose loading problems, consider using a tool like strace. If you're using glibc, consider setting the LD_DEBUG environment variable: 
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/bass.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/bass.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/bass.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libbass.so: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libbass.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libbass.so: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/bass: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/bass: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/bass: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/runtimes/linux-x64/native/libbass: cannot open shared object file: No such file or directory
/home/andreas/.dotnet/shared/Microsoft.NETCore.App/7.0.2/libbass: cannot open shared object file: No such file or directory
/home/andreas/projects/toneUI/toneUI/toneUI.Desktop/bin/Debug/net7.0/libbass: cannot open shared object file: No such file or directory

             */
            ReleaseChannel();
            /*
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                Status = PlaybackStatus.Error;
                MediaError?.Invoke(this, EventArgs.Empty);
            });
            */
        }
    }
    
    
    private void ReleaseChannel()
    {
        /*
        if (BassActive)
        {
            ManagedBass.Bass.ChannelStop(_chanOut).Valid();
            ManagedBass.Bass.StreamFree(_chanOut).Valid();
            _chanOut = 0;
            _chanMix = 0;
            _chanIn = 0;
        }
        */
    }


    void SharpAudioPlaySound(string url)
    {
        ShouldStop = false;

        var webRequest = WebRequest.Create(url);

        using var response = webRequest.GetResponse();
        using var content = response.GetResponseStream();
        using var engine = AudioEngine.CreateDefault();
        if (engine == null)
        {
            Console.WriteLine("Failed to create an audio backend!");
            return;
        }
        
        

        var soundStream = new SoundStream(content, engine);

        soundStream.Volume = 1;

        soundStream.Play();

        Console.WriteLine("Playing file with duration: " + soundStream.Duration);

        while (soundStream.IsPlaying)
        {
            Thread.Sleep(100);
            if (ShouldStop)
            {
                soundStream.Stop();
                break;
            }
        }
    }
    
    void PlaySound()
    {
        
        Console.WriteLine("PlaySound called");
         
        var url = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3";
        
        // SharpAudioPlaySound(url);
        // BassPlaySound(url);
        VlcSharpPlaySound(url);
    }

    bool CanPlaySound()
    {
        return true;
    }

    bool CanStopSound()
    {
        return true;
    }
    void StopSound()
    {
        Console.WriteLine("StopSound called");

        ShouldStop = true;
    }
}