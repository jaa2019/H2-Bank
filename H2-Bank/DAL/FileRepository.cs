using System;
using H2_Bank.Repository;

namespace H2_Bank.DAL
{
    public class FileRepository : iFileRepository
    {
        const string fileName = "datafile.data";

        public FileRepository()
        {

        }
    }
}
