using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_Message
    {
        UnitOfWork unitOfWork;

        public cls_Message()
        {
            unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Replace one text message in email template and other message format..
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="replaceFrom"></param>
        /// <param name="replaceTo"></param>
        /// <returns>Replace message Text</returns>
        public string GetMessage(EnumConstants.Message messageType,string replaceFrom,string replaceTo)
        {
            var message = unitOfWork.MessageRepository.Find(f => f.MessageType == (byte)messageType).Message.Replace(replaceFrom, replaceTo).ToString();
            return message;
        }

        /// <summary>
        /// Replace multiple text message in email template and other message format..
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="replaceItems"></param>
        /// <returns></returns>
        public string GetMessage(EnumConstants.Message messageType,Dictionary<string,string> replaceItems)
        {
            string message =unitOfWork.MessageRepository.Find(f => f.MessageType == (byte)messageType).Message;
            foreach (KeyValuePair<string, string> item in replaceItems)
            {
                message = message.Replace(item.Key, item.Value).ToString();
            }
           
            return message;
        }
    }
}
