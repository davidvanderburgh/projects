using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace High_Entropy_Passphrases
{
    public class HighEntropyPassphrase
    {
        private List<string> passPhrasePhrases = new List<string>();

        public string PassPhrase { get; set; }
        public List<string> PassPhrasePhrases
        {
            get
            {
                return PassPhrase.Split(' ').ToList();
            }
            set
            {
                passPhrasePhrases = value;
            }
        }

        public HighEntropyPassphrase()
        {

        }

        public HighEntropyPassphrase(string passPhrase)
        {
            PassPhrasePhrases = passPhrase.Split(' ').ToList();
        }

        public bool IsValid()
        {
            return (PassPhrasePhrases.Count == PassPhrasePhrases.Distinct().ToList().Count);
        }
    }
}
