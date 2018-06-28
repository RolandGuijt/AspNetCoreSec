using Microsoft.AspNetCore.Authorization;

namespace AspNetSecurity_m4.Authorization
{
    public class ProposalRequirement: IAuthorizationRequirement
    {
        public ProposalRequirement(bool mustbeApproved)
        {
            MustBeApproved = mustbeApproved;
        }
        public bool MustBeApproved { get; set; }
    }
}
