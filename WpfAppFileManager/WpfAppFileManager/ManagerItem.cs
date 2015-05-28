using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppFileManager
{
    public class ManagerItem
    {
        public bool isDirectory { get; set; }
        public String Name { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }

        public ManagerItem(bool isDirectory, String name, long size, DateTime date)
        {
            this.isDirectory = isDirectory;
            this.Name = name;
            this.Size = size;
            this.Date = date;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
