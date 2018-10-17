using System;


namespace UnityCairo
{
    class Surface : IDisposable
    {
        public IntPtr Ptr
        {
            get;
            private set;
        }

        Surface(IntPtr p)
        {
            Ptr = p;
        }

        public static Surface Create(int w, int h)
        {
            var p = Dll.cairo_image_surface_create(cairo_format_t.CAIRO_FORMAT_ARGB32, w, h);
            if (p == IntPtr.Zero) return null;

            return new Surface(p);
        }

        public static Surface CreateFromBytes(Byte[] data, int width, int height, int stride)
        {
            var p = Dll.cairo_image_surface_create_for_data(data,
                                                 cairo_format_t.CAIRO_FORMAT_ARGB32,
                                                 width,
                                                 height,
                                                 stride);
            if (p == IntPtr.Zero) return null;
            return new Surface(p);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。
                if (Ptr != IntPtr.Zero)
                {
                    Dll.cairo_surface_destroy(Ptr);
                    Ptr = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~Surface() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
