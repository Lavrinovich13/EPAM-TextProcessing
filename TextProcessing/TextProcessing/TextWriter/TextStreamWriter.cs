using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    class TextStreamWriter : IWriter
    {
        public void Write(StreamWriter writer, Text text)
        {
            Type lastType = null;
            bool isClose = false;
            string lostSpace = String.Empty;

            using (writer)
            {
                foreach (var sentence in text.GetSentences())
                {
                    foreach (var item in sentence._Items)
                    {
                        if (item is IWord)
                        {
                            if (lastType == typeof(IWord))
                            {
                                writer.Write(" ");
                            }
                            if (lastType == typeof(IDelimeter))
                            {
                                writer.Write(lostSpace);
                            }

                            writer.Write(item._StringValue);
                            lastType = typeof(IWord);
                            continue;
                        }
                        if (item is IDelimeter)
                        {
                            var delimeter = (item as IDelimeter);
                            if (lastType == typeof(IWord))
                            {
                                if (delimeter._IsSeparatedBySpace
                                    || delimeter._DelimeterType == DelimeterTypes.FirstInPair
                                    || (delimeter._DelimeterType == DelimeterTypes.Double && !isClose))
                                {
                                    lostSpace = String.Empty;
                                    writer.Write(" ");
                                }

                                if (delimeter._DelimeterType == DelimeterTypes.Single
                                    || delimeter._DelimeterType == DelimeterTypes.LastInPair
                                    || (delimeter._DelimeterType == DelimeterTypes.Double) && isClose)
                                {
                                    lostSpace = " ";
                                }
                            }
                            else
                                if (lastType == typeof(IDelimeter)
                                && (delimeter._IsSeparatedBySpace
                                || delimeter._DelimeterType == DelimeterTypes.FirstInPair
                                || (delimeter._DelimeterType == DelimeterTypes.Double && !isClose)))
                                {
                                    writer.Write(lostSpace);
                                    lostSpace = String.Empty;
                                }


                            if (delimeter._DelimeterType == DelimeterTypes.Double)
                            {
                                isClose = !isClose;
                            }

                            writer.Write(item._StringValue);
                            lastType = typeof(IDelimeter);
                        }
                    }
                }
            }
        }
    }
}
