using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SampleATSPlugin;

public static class ATSPlugin
{
	const CallingConvention callingConvention = CallingConvention.StdCall;
	static HandlePos handlePos;

	static readonly SampleClass sampleClass = new();

	static ATSPlugin()
	{
#if DEBUG
		if (!Debugger.IsAttached)
			Debugger.Launch();
#endif

		AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
	}

	private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
	{
		AssemblyName assemblyName = new(args.Name);

		string currentDllLocation = Assembly.GetExecutingAssembly().Location;

		string pathToFind = Path.Combine(Path.GetDirectoryName(currentDllLocation), assemblyName.Name + ".dll");

		if (!File.Exists(pathToFind))
			return null;

		return Assembly.Load(pathToFind);
	}

	[DllExport(CallingConvention = callingConvention)]
	public static HandlePos Elapse(State s, IntPtr Pa, IntPtr So)
	{
		sampleClass.SetIntValue(Pa, 0, s.Time / 1000);
		return handlePos;
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void Dispose()
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void DoorClose()
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void DoorOpen()
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static uint GetPluginVersion()
	{
		return ATSConstantValues.VERSION;
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void HornBlow(int k)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void Initialize(int s)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void KeyDown(int k)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void KeyUp(int k)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void Load()
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetBeaconData(Beacon b)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetBrake(int b)
	{
		handlePos.Brake = b;
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetPower(int p)
	{
		handlePos.Power = p;
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetReverser(int r)
	{
		handlePos.Reverser = r;
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetSignal(int s)
	{
	}

	[DllExport(CallingConvention = callingConvention)]
	public static void SetVehicleSpec(Spec s)
	{

	}
}

[StructLayout(LayoutKind.Sequential)]
public struct Spec
{
	public int Brake;
	public int Power;
	public int Ats;
	public int B67;
	public int Cars;
};

[StructLayout(LayoutKind.Sequential)]
public struct State
{
	public double Location;
	public float Speed;
	public int Time;
	public float BC;
	public float MR;
	public float ER;
	public float BP;
	public float SAP;
	public float Current;
};

[StructLayout(LayoutKind.Sequential)]
public struct HandlePos
{
	public int Brake;
	public int Power;
	public int Reverser;
	public int ConstSpeed;
};

[StructLayout(LayoutKind.Sequential)]
public struct Beacon
{
	public int Num;
	public int Sig;
	public float Distance;
	public int Data;
};

public static class ATSConstantValues
{
	public const int VERSION = 0x20000;

	public const int KEY_S = 0;
	public const int KEY_A1 = 1;
	public const int KEY_A2 = 2;
	public const int KEY_B1 = 3;
	public const int KEY_B2 = 4;
	public const int KEY_C1 = 5;
	public const int KEY_C2 = 6;
	public const int KEY_D = 7;
	public const int KEY_E = 8;
	public const int KEY_F = 9;
	public const int KEY_G = 10;
	public const int KEY_H = 11;
	public const int KEY_I = 12;
	public const int KEY_J = 13;
	public const int KEY_K = 14;
	public const int KEY_L = 15;

	public const int INIT_B67 = 0;
	public const int INIT_EB = 1;
	public const int INIT_REMOVED = 2;

	public const int SOUND_STOP = -10000;
	public const int SOUND_PLAY = 1;
	public const int SOUND_LOOP = -1;
	public const int SOUND_CONTINUE = 2;

	public const int HORN_1 = 0;
	public const int HORN_2 = 1;
	public const int HORN_MUSIC = 2;

	public const int CONSTSPD_CONTINUE = 0;
	public const int CONSTSPD_ENABLE = 1;
	public const int CONSTSPD_DISABLE = 2;
}
