using DTO;

namespace DAL.Adapters
{
    public class RegionAdapter
    {
        public RegionDTO? ToDTO(Region? region)
        {
            if (region == null)
            {
                return null;
            }
            return new RegionDTO
            {
                Id = region.Id,
                Number = region.Number
            };
        }

        public Region? ToDAL(RegionDTO? region)
        {
            if (region == null)
            {
                return null;
            }
            return new Region
            {
                Id = region.Id,
                Number = region.Number
            };
        }
    }
}