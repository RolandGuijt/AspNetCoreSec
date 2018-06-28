using Microsoft.AspNetCore.Authorization;

namespace AspNetSecurity_m4.Authorization
{
    public class YearsOfExperienceRequirement: IAuthorizationRequirement
    {
        public YearsOfExperienceRequirement(int yearsOfExperienceRequired)
        {
            YearsOfExperienceRequired = yearsOfExperienceRequired;
        }
        public int YearsOfExperienceRequired { get; set; }
    }
}
