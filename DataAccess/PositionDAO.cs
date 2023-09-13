using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PositionDAO
    {

        private static PositionDAO instance = null;
        private static readonly object instanceLock = new object();

        public static PositionDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PositionDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Position> GetPositions()
        {
            List<Position> positions;
            try
            {
                var dbContext = new MiniStoreContext();
                positions = dbContext.Positions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return positions;
        }

        public Position GetPositionById(int posId)
        {
            Position position;
            try
            {
                var dbContext = new MiniStoreContext();
                position = dbContext.Positions.Find(posId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return position;
        }
    }
}
