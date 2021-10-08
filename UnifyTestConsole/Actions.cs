using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unify;
using Unify.DataStructures;
using Unify.Helpers;

namespace UnifyTestConsole
{
    internal static class Actions
    {
        public static void Action1(Connection connection, ByteBuffer buffer)
        {
            var info = new BasicInfo();
            info.Serialize<BasicInfo>(ref buffer);
            
        }
        public static void Action2(Connection connection, ByteBuffer buffer)
        {

        }
        // ReSharper disable once InvalidXmlDocComment
        /**public static void {ACTION_NAME}(Connection connection, Bytebuffer buffer){
         * {
         *  var info = new {PACKET_INFO_NAME}();
         *  info.Serialize<{PACKET_INFO_NAME}>(ref buffer);
         *
         *      \//Your Code here!
         *
         *}  
         */
        
    }
}
