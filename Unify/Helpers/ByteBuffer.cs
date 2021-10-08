using System;
using System.Reflection;
using System.Text;

namespace Unify.Helpers
{
    // ReSharper disable once ClassTooBig
    public class ByteBuffer
    {
        private int _head;
        private byte[] _data;

        public ByteBuffer(int size=0)
        {
            _head = 0;
            _data = new byte[size];
        }
        public ByteBuffer(byte[] data)
        {
            _head = 0;
            _data = data;
        }

        #region TypeMethods

        public void Write(TypeCode code, object obj)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    break;
                case TypeCode.Object:
                    break;
                case TypeCode.DBNull:
                    break;
                case TypeCode.Boolean:
                    WriteBool((bool)obj);
                    break;
                case TypeCode.Char:
                    WriteChar((char) obj);
                    break;
                case TypeCode.SByte:
                    WriteSByte((sbyte) obj);
                    break;
                case TypeCode.Byte:
                    WriteByte((byte) obj);
                    break;
                case TypeCode.Int16:
                    WriteInt16((short) obj);
                    break;
                case TypeCode.UInt16:
                    WriteUInt16((ushort) obj);
                    break;
                case TypeCode.Int32:
                    WriteInt32((int) obj);
                    break;
                case TypeCode.UInt32:
                    WriteUInt32((uint) obj);
                    break;
                case TypeCode.Int64:
                    WriteInt64((long) obj);
                    break;
                case TypeCode.UInt64:
                    WriteUInt64((ulong) obj);
                    break;
                case TypeCode.Single:
                    WriteFloat((float) obj);
                    break;
                case TypeCode.Double:
                    WriteDouble((double) obj);
                    break;
                case TypeCode.Decimal:
                    break;
                case TypeCode.DateTime:
                    break;
                case TypeCode.String:
                    WriteString((string) obj);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, null);
            }
        }

        public dynamic Read(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Empty:
                    throw new ArgumentOutOfRangeException();
                case TypeCode.Object:
                    throw new ArgumentOutOfRangeException();
                case TypeCode.DBNull:
                    throw new ArgumentOutOfRangeException();
                case TypeCode.Boolean:
                    return ReadBool();
                case TypeCode.Char:
                    return ReadChar();
                case TypeCode.SByte:
                    return ReadSByte();
                case TypeCode.Byte:
                    return ReadByte();
                case TypeCode.Int16:
                    return ReadInt16();
                case TypeCode.UInt16:
                    return ReadUInt16();
                case TypeCode.Int32:
                    return ReadInt32();
                case TypeCode.UInt32:
                    return ReadUInt32();
                case TypeCode.Int64:
                    return ReadInt64();
                case TypeCode.UInt64:
                    return ReadUInt64();
                case TypeCode.Single:
                    return ReadFloat();
                case TypeCode.Double:
                    return ReadDouble();
                case TypeCode.Decimal:
                    throw new ArgumentOutOfRangeException();
                case TypeCode.DateTime:
                    throw new ArgumentOutOfRangeException();
                case TypeCode.String:
                    return ReadString();
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        #endregion

        #region WriteMethods

        public void WriteInt16(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteInt32(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteInt64(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteUInt32(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteUInt16(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteUInt64(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteByte(byte value)
        {
            var bytes = new [] { value };
            MergeArrays(bytes);
        }
        public void WriteSByte(sbyte value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteChar(char value)
        {
            var bytes = new [] { (byte)value };
            MergeArrays(bytes);
        }
        public void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteFloat(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }
        public void WriteBytes(byte[] value)
        {
            var length = value.Length;
            MergeArrays(BitConverter.GetBytes(length));
            MergeArrays(value);
        }
        public void WriteString(string value)
        {
            var length = value.Length;
            MergeArrays(BitConverter.GetBytes(length));
            MergeArrays(Encoding.ASCII.GetBytes(value));
        }
        public void WriteBool(bool value)
        {
            var bytes = BitConverter.GetBytes(value);
            MergeArrays(bytes);
        }

        #endregion

        #region ReadMethods

        public short ReadInt16()
        {
            _head += 2;
            return BitConverter.ToInt16(_data, _head - 2);
        }

        public int ReadInt32()
        {
            _head += 4;
            return BitConverter.ToInt32(_data, _head - 4);
        }
        public long ReadInt64()
        {
            _head += 8;
            return BitConverter.ToInt64(_data, _head - 8);
        }

        public ushort ReadUInt16()
        {
            _head += 2;
            return BitConverter.ToUInt16(_data, _head - 2);
        }

        public uint ReadUInt32()
        {
            _head += 4;
            return BitConverter.ToUInt32(_data, _head - 4);
        }
        public ulong ReadUInt64()
        {
            _head += 8;
            return BitConverter.ToUInt64(_data, _head - 8);
        }

        public bool ReadBool()
        {
            _head += 1;
            return BitConverter.ToBoolean(_data, _head - 1);
        }
        public byte ReadByte()
        {
            _head += 1; 
            return _data[_head - 1];
        }

        public byte[] ReadBytes()
        {
            var length = ReadInt32();
            var bytes = new byte[length];
            Buffer.BlockCopy(_data, _head, bytes, 0, length);
            _head += length;
            return bytes;
        }

        public byte[] ReadBlock(int count)
        {
            var bytes = new byte[count];
            Buffer.BlockCopy(_data, _head, bytes, 0, count);
            _head += count;
            return bytes;
        }

        public byte ReadSByte()
        {
            _head += 1;
            return _data[_head - 1];
        }
        public char ReadChar()
        {
            _head += 1;
            return (char)_data[_head - 1];
        }
        public double ReadDouble()
        {
            _head += 8;
            return BitConverter.ToDouble(_data, _head - 8);
        }
        public float ReadFloat()
        {
            _head += 4;
            return BitConverter.ToSingle(_data, _head - 4);
        }

        public string ReadString()
        {
            var length = ReadInt32();
            var bytes = new byte[length];
            Buffer.BlockCopy(_data,_head,bytes,0,length);
            _head += length;
            return Encoding.ASCII.GetString(bytes);
        }
        #endregion

        #region PublicMethods

        public int GetHead()
        {
            return _head;
        }

        public byte[] GetData()
        {
            return _data;
        }

        #endregion

        #region PrivateMethods

        private void MergeArrays(byte[] bytes)
        {
            var newData = new byte[_data.Length + bytes.Length];
            Array.Copy(_data, newData, _data.Length);
            Array.Copy(bytes, 0, newData, _data.Length, bytes.Length);
            _head += bytes.Length;
            _data = newData;
        }

        #endregion


    }
}