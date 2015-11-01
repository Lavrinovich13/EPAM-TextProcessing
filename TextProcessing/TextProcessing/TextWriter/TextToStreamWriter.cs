using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextProcessing
{
    public class TextToStreamWriter : IStreamWriter<Text>
    {
        public IDelimeter SpaceDelimeter { get; set; }

        public void Write(StreamWriter writer, Text text)
        {
            Type lastType = null;
            bool isClose = false;
            string followedSpace = String.Empty;

            foreach (var sentence in text.GetSentences)
            {
                foreach (var item in sentence.Items)
                {
                    if (item is IWord)
                    {
                        if (lastType == typeof(IWord))
                        {
                            writer.Write(SpaceDelimeter.StringValue);
                        }
                        if (lastType == typeof(IDelimeter))
                        {
                            writer.Write(followedSpace);
                        }

                        writer.Write(item.StringValue);
                        lastType = typeof(IWord);
                        continue;
                    }
                    if (item is IDelimeter)
                    {
                        var delimeter = (item as IDelimeter);
                        if (lastType == typeof(IWord))
                        {
                            if (delimeter.IsSeparatedBySpace
                                || delimeter.DelimeterType == DelimeterTypes.FirstInPair
                                || (delimeter.DelimeterType == DelimeterTypes.Double && !isClose))
                            {
                                followedSpace = String.Empty;
                                writer.Write(SpaceDelimeter.StringValue);
                            }

                            if (delimeter.DelimeterType == DelimeterTypes.Single
                                || delimeter.DelimeterType == DelimeterTypes.LastInPair
                                || (delimeter.DelimeterType == DelimeterTypes.Double) && isClose)
                            {
                                followedSpace = SpaceDelimeter.StringValue;
                            }
                        }
                        else
                            if (lastType == typeof(IDelimeter)
                            && (delimeter.IsSeparatedBySpace
                            || delimeter.DelimeterType == DelimeterTypes.FirstInPair
                            || (delimeter.DelimeterType == DelimeterTypes.Double && !isClose)))
                            {
                                writer.Write(followedSpace);
                                followedSpace = String.Empty;
                            }


                        if (delimeter.DelimeterType == DelimeterTypes.Double)
                        {
                            isClose = !isClose;
                        }

                        writer.Write(item.StringValue);
                        lastType = typeof(IDelimeter);
                    }
                }
            }
        }
    }
}
