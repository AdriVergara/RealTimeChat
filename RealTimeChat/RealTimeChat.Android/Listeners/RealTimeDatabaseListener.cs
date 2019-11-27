//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;

//namespace RealTimeChat.Droid.Listeners
//{
//    public class RealTimeDatabaseListener : Java.Lang.Object, IValueEventListener
//    {
//        FirebaseManager mFirebaseManager;

//        public ReadListener(FirebaseManager manager)
//        {
//            mFirebaseManager = manager;
//        }

//        public void OnCancelled(DatabaseError error)
//        {
//            Debug.WriteLine("ERROR: " + error, "LOADDATA");
//        }

//        public void OnDataChange(DataSnapshot snapshot)
//        {
//            if (snapshot == null) return;

//            var snapChildren = snapshot.Children;
//            var iterator = snapChildren.Iterator();
//            var items = new List<DataItem>();

//            while (iterator.HasNext)
//            {
//                var item = iterator.Next();
//                // item is a Java.Lang.Object, so how can I change it to DataItem?
//                // I've tried to create a DataItemJava : Java.Lang.Object, 
//                // and have var item = (DataItemJava) iterator.Next();
//                // but I get a System.InvalidCastException
//            }
//        }

//    }
//}