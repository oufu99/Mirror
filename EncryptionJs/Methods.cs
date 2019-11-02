using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionJs
{
    public static class Methods
    {
        public static Task LoadPageAsync(this ChromiumWebBrowser browser, string address = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler<IsBrowserInitializedChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                if (args.IsBrowserInitialized)
                {
                    browser.IsBrowserInitializedChanged -= handler;
                    browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("alert('haha')");


                    tcs.TrySetResult(true);
                }
            };

            browser.IsBrowserInitializedChanged += handler;
            return tcs.Task;
        }
    }
}
