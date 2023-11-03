using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Users.ValueObjects;

namespace SimpleShopD.Infrastructure.EF.Models
{
    internal sealed class UserReadModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public AccountStatus StatusOfAccount { get; set; }
        public Role RoleOfUser { get; set; }
        public HashSet<AddressReadModel> Addresses { get; set; }
        public string ActivationToken { get; set; }
        public DateTime ActivationTokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime ResetPasswordTokenExpirationDate { get; set; }
    }
}
