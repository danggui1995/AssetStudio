﻿using AssetStudio.FbxInterop;
using AssetStudio.PInvoke;
using System.IO;

namespace AssetStudio
{
    public static partial class Fbx
    {

        static Fbx()
        {
            DllLoader.PreloadDll(FbxDll.DllName);
        }

        public static Vector3 QuaternionToEuler(Quaternion q)
        {
            AsUtilQuaternionToEuler(q.X, q.Y, q.Z, q.W, out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }

        public static Quaternion EulerToQuaternion(Vector3 v)
        {
            AsUtilEulerToQuaternion(v.X, v.Y, v.Z, out var x, out var y, out var z, out var w);
            return new Quaternion(x, y, z, w);
        }

        public static class Exporter
        {
            public static void Export(string path, IImported imported, bool eulerFilter, float filterPrecision,
                bool allNodes, bool skins, bool animation, bool blendShape, bool castToBone, float boneSize, bool exportAllUvsAsDiffuseMaps, float scaleFactor, int versionIndex, bool isAscii)
            {
                var dir = Path.GetDirectoryName(path);
                Directory.CreateDirectory(dir);
                
                var exporter = new FbxExporter(path, imported, allNodes, skins, castToBone, boneSize,
                    exportAllUvsAsDiffuseMaps, scaleFactor, versionIndex, isAscii);
                {
                    exporter.Initialize();
                    exporter.ExportAll(blendShape, animation, eulerFilter, filterPrecision);
                    exporter.Dispose();
                }
            }

        }
    }
}
