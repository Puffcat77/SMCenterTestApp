using SMCenterTestApp.DAL;
using SMCenterTestApp.DTO;

namespace SMCenterTestApp.Adapters
{
    public class CabinetAdapter
    {
        public Cabinet ToDAL(CabinetDTO cabinet)
        {
            if (cabinet == null)
            {
                return null;
            }
            return new Cabinet()
            {
                Number = cabinet.Number
            };
        }

        public CabinetDTO ToDTO(Cabinet cabinet)
        {
            if (cabinet == null)
            {
                return null;
            }
            return new CabinetDTO()
            {
                Id = cabinet.Id,
                Number = cabinet.Number
            };
        }
    }
}