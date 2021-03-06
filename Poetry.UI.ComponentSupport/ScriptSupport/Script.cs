﻿namespace Poetry.UI.ScriptSupport
{
    public class Script
    {
        public string Path { get; }
        public string LocalPath { get; }

        public Script(string componentId, string path)
        {
            Path = componentId + "/" + path;
            LocalPath = path;
        }
    }
}