﻿using System;
using System.IO;

namespace Ypf_Manager
{
    abstract class Checksum
    {

        //
        // Variable(s)
        //

        public abstract String Name { get; }


        //
        // Function(s)
        //

        // Compute byte array hash with implemented algorithm
        public abstract UInt32 ComputeHash(Byte[] inputBytes);


        // Compute stream hash with implemented algorithm
        public UInt32 ComputeHash(Stream inputStream, int length)
        {
            Byte[] buffer = new Byte[length];

            inputStream.Read(buffer, 0, length);

            return ComputeHash(buffer);
        }

    }
}
