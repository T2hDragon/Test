namespace PublicAPI.DTO.v1.Mappers
{
    public class ContactMapper
    {
        public static BLL.App.DTO.Contact MapToBll(PublicAPI.DTO.v1.Contact contact)
        {
            return new BLL.App.DTO.Contact()
            {
                Value = contact.Value,
                ContactTypeId = contact.ContactTypeId
            };
        }
}
}