using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Words
{
    public class BaseController: Controller
    {
        public WordsEntities db = new WordsEntities();
        List<cetsix> list;
        public List<cetsix> getWords()
        {
            if (list == null)
            {
                list = db.cetsix.Where(p => p.words.Length > 1).ToList();
            }
            return list;
        }
        public List<cetsix> getchildSimilar(cetsix word)
        {
            List<cetsix> ret = new List<cetsix>();
            List<cetsix> temp = getWords();
            int length = word.words.Length;
            temp.Remove(word);
            while (length > 3)
            {
                for (var index = 0; index < word.words.Length - length + 1; index++)
                {
                    var c = temp.Where(p => word.words.Substring(index, length).Contains(p.words)).ToList();
                    foreach (var i in c)
                    {
                        temp.Remove(i);
                        ret.Add(i);
                    }
                }
                length--;
            }
            return ret.OrderBy(p => p.words).ToList();
        }
        public List<cetsix> getparentSimilar(cetsix word)
        {
            List<cetsix> ret = new List<cetsix>();
            List<cetsix> temp = getWords();
            temp.Remove(word);
            temp = temp.Where(p => p.words.Contains(word.words)).ToList();
            foreach (var i in temp)
            {

                ret.Add(i);
            }
            return ret.OrderBy(p => p.words).ToList();
        }
    }
}