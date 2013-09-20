using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beryllium.lib.Nbt {
   public abstract class NbtTagPayload {
      public abstract string ToDebugStringShort();
   }


   public abstract class NbtTagPayload_Composite : NbtTagPayload {
      public abstract IEnumerable<NbtTagInfo> EnumTags();
   }


   public abstract class NbtTagPayload_Scalar<T> : NbtTagPayload {
      public abstract T GetValue();
   }


   internal sealed class NbtTagPayload_End : NbtTagPayload {
      public override string ToDebugStringShort() {
         return "(no payload)";
      }
   }


   internal sealed class NbtTagPayload_Byte : NbtTagPayload_Scalar<sbyte> {
      public sbyte Value { get; private set; }
      public NbtTagPayload_Byte(sbyte value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override sbyte GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_Short : NbtTagPayload_Scalar<short> {
      public short Value { get; private set; }
      public NbtTagPayload_Short(short value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override short GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_Int : NbtTagPayload_Scalar<int> {
      public int Value { get; private set; }
      public NbtTagPayload_Int(int value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override int GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_Long : NbtTagPayload_Scalar<long> {
      public long Value { get; private set; }
      public NbtTagPayload_Long(long value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override long GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_Float : NbtTagPayload_Scalar<float> {
      public float Value { get; private set; }
      public NbtTagPayload_Float(float value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override float GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_Double : NbtTagPayload_Scalar<double> {
      public double Value { get; private set; }
      public NbtTagPayload_Double(double value) { Value = value; }
      public override string ToDebugStringShort() { return Value.ToString(); }
      public override double GetValue() { return Value; }
   }


   internal sealed class NbtTagPayload_String : NbtTagPayload_Scalar<string> {
      public string Value { get; private set; }
      public NbtTagPayload_String(string value) { Value = value; }
      public override string ToDebugStringShort() { return Value; }
      public override string GetValue() { return Value; }
   }
}
