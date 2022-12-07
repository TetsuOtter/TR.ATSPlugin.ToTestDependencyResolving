using System;
using System.Runtime.InteropServices;

using SampleLibrary;

namespace SampleATSPlugin;

internal class SampleClass : IInterface1
{
		public void SetIntValue(IntPtr target, int index, int value)
				=> Marshal.WriteInt32(target, index * sizeof(int), value);
}
