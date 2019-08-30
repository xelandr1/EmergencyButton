//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Gms.Maps;
//using Android.Graphics;
//using Android.Hardware;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;

//namespace EmergencyButton.App.Droid.MapView
//{
//    class Class1
//    {
//    }

//    public class MapView1 : ViewGroup, ISurfaceHolderCallback
//    {
//        SurfaceView surfaceView;
//        ISurfaceHolder holder;
//        IWindowManager windowManager;

//        MapFragment map;


//        public MapView1(Context context) : base(context)
//        {
//            surfaceView = new SurfaceView(context);
//            AddView(surfaceView);

//            windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
//            holder = surfaceView.Holder;
//            holder.AddCallback(this);
//            map = new MapFragment()
//            {
//            };

//        }

//        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
//        {
//            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
//        }

//        protected override void OnLayout(bool changed, int l, int t, int r, int b)
//        {
//            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
//            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

//            surfaceView.Measure(msw, msh);
//            surfaceView.Layout(0, 0, r - l, b - t);

//        }

//        public void SurfaceCreated(ISurfaceHolder holder)
//        {
//            try
//            {
//                if (map != null)
//                {
//                    map.(holder);
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
//            }
//        }
//        public void SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
//        {
//            throw new NotImplementedException();
//        }


//        public void SurfaceDestroyed(ISurfaceHolder holder)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}