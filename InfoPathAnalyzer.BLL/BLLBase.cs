using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.BLL
{
    public abstract class BLLBaseWithDAL<T>
    {
        private T dal;

        public T DataAccessLayer
        {
            get
            {
                return this.dal;
            }
            set
            {
                this.dal = value;
            }
        }

        protected void ValidateDataAccessLayer()
        {
            if (this.dal == null)
            {
                throw new MemberAccessException("DataAccessLayeer cannot be null.");
            }
        }
    }
}
