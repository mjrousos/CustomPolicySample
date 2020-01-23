using Microsoft.AspNetCore.Authorization;

namespace CustomPolicyProvider
{
    // This attribute derives from the [Authorize] attribute, adding 
    // the ability for a user to specify a project name paratmer. Since authorization
    // policies are looked up from the policy provider only by string, this
    // authorization attribute creates is policy name based on a constant prefix
    // and the user-supplied project parameter. A custom authorization policy provider
    // (`ProjectPolicyProvider`) can then produce an authorization policy with 
    // the necessary requirements based on this policy name.
    internal class ProjectAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Project";

        public ProjectAuthorizeAttribute(string projectName) => ProjectName = projectName;

        // Get or set the ProjectName property by manipulating the underlying Policy property
        public string ProjectName
        {
            get
            {
                return Policy.Substring(POLICY_PREFIX.Length);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
}