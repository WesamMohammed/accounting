using AutoMapper;
using jwt.PermissionHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Services
{
    public class UsersRolesPermissionsService:IUsersRolesPermissionsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public UsersRolesPermissionsService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IHttpContextAccessor contextAccessor,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        public async Task<UserInfo> GetUserInf(string userId)
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var rolePermissions = new List<string>();
            foreach (var role in userRoles)
            {
                var rolee = await _roleManager.FindByNameAsync(role);
                var permissions = await _roleManager.GetClaimsAsync(rolee);
                rolePermissions.AddRange(permissions.Select(a => a.Value).ToList());
            


            }

            var userInf = new UserInfo()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email=user.Email,
                Roles=userRoles.ToList(),
                Permissions=rolePermissions,
            };
           
            return userInf;
        }
        public async Task<List<User>> GetAllOtherUsers()
        {
            var allotherUsers = new List<ApplicationUser>();
            /*if (_contextAccessor.HttpContext.User != null&&false) { */
            var claim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "uid");
            var currentUser = await _userManager.Users.FirstOrDefaultAsync(a=>a.Id==claim.Value);
            allotherUsers = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            
           /* else
            {
                allotherUsers = await _userManager.Users.ToListAsync();
            }*/
            return _mapper.Map<List<User>>(allotherUsers) ;


        }
        public async Task<ManageUserRolesViewModel> GetUserRoles(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userrolesviewmodel = new UserRolesViewModel()
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrolesviewmodel.Selected = true;
                }
                viewModel.Add(userrolesviewmodel);

            }
            var resutl = new ManageUserRolesViewModel { UserId = user.Id,UserName=user.UserName,Email=user.Email, userRoles = viewModel };
            return resutl;
        }


        public async Task<bool> UpdateUserRoles(ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userroles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, userroles);
            result = await _userManager.AddToRolesAsync(user, model.userRoles.Where(a => a.Selected).Select(a => a.RoleName));
            await DefaultUser.SeedAdminUserAsync(_userManager, _roleManager);
            return true;

        }
        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<Role>>(roles) ;
        }

        public async Task<IdentityRole>AddRole(ManageRolePermissionsViewModel model)
        {
            var role = new IdentityRole(model.RoleName);
            if(model.RoleName is not null)
            {
                await _roleManager.CreateAsync(role);
                model.RoleId = role.Id;
                var result= await UpdateRolePermissions(model);
            }
            return role;
        }

        public async Task<ManageRolePermissionsViewModel> GetRolePermissions(string? RoleId)

        {
            var model = new ManageRolePermissionsViewModel();
            var AllPermissions = new List<RolePermissionsViewModel>();

            var role = await _roleManager.Roles.FirstOrDefaultAsync(a=>a.Id==RoleId);
            var RolePermissions = new List<Claim>();
            if(role is not null)
            {
                 RolePermissions = (List<Claim>)await _roleManager.GetClaimsAsync(role);

            }
           
            
            var pages = typeof(AppPermissions);
            var AllPages = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Namespace == pages.Namespace).ToList();
            for(int i = 1; i < AllPages.Count; i++)
            {
                AllPermissions.GetPermissions(AllPages[i], RoleId);
            }
           
            model.RoleId = role?.Id;
            model.RoleName = role?.Name;
            var AllPermissionsValues = AllPermissions.Select(a => a.Value).ToList();
            var RolePermissionsValues = RolePermissions.Select(a => a.Value).ToList();
            var gevenPermissions = AllPermissionsValues.Intersect(RolePermissionsValues);

            foreach(var permission in AllPermissions)
            {
                if (gevenPermissions.Any(a => a == permission.Value))
                    {
                    permission.Selected = true;
                    }

            }
            model.RolePermissions = AllPermissions;
            return model;

        }
        public async Task<bool> UpdateRolePermissions(ManageRolePermissionsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var rolePermissions = await _roleManager.GetClaimsAsync(role);
            foreach (var rolePermission in rolePermissions)
            {
                await _roleManager.RemoveClaimAsync(role, rolePermission);
            }
            var selectedPermissions = model.RolePermissions.Where(a => a.Selected==true).ToList();
            foreach (var selectedPermission in selectedPermissions)
            {
                await _roleManager.AddPermission(role, selectedPermission.Value);
            }
            return true;
        }
    }
}
