using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class PositionRepository : IPositionRepository
    {
        public IEnumerable<Position> GetPositions() => PositionDAO.Instance.GetPositions();
        public Position GetPositionById(int posId) => PositionDAO.Instance.GetPositionById(posId);
    }
}
