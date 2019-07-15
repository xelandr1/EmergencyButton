using System;
using Android.Content;

namespace EmergencyButton.App.Droid.Services
{
    public interface ICurrentContext
    {
        Context Context { get; set; }
    }

    public class CurrentContextService: ICurrentContext
    {
        WeakReference<Context> _context;

        public Context Context
        {
            get
            {
                Context context;

                if (_context.TryGetTarget(out context))
                {
                    return context;
                }
                return null;
            }
            set
            {
                _context = new WeakReference<Context>(value);
            }
        }

        public CurrentContextService(Context context)
        {
            Context = context;
        }
    }
}