﻿using OneTransfert.wasm.Enums;

namespace OneTransfert.wasm.Models;
internal class FileTransferInfo : FileMetadata
{
    public List<byte> FileContext { get; set; } = new List<byte>();
    public FileTransferStateEnum State { get; set; }
    public double Progress { get; set; }
    public bool Succeed { get; set; }
    public string Message { get; set; } = "";
}

internal class FileMetadata
{
    public string FileName { get; set; } = null!;
    public string SHA1 { get; set; } = null!;
    public int FileSize { get; set; }
}