namespace FE_Assessment.Pages
{
    public static class HomePage
    {

        //HOME PAGE
        public static readonly string add_btn = "//button/i[contains(@class, 'icon-plus')]";
        public static readonly string search_input = "//td/input[contains(@class,'pull-right')]";


        //ADD USER MODAL
        public static readonly string firstname_input = "//td/input[@name = 'FirstName']";
        public static readonly string lastname_input = "//td/input[@name = 'LastName']";
        public static readonly string username_input = "//td/input[@name = 'UserName']";
        public static readonly string password_input = "//td/input[@name = 'Password']";
        public static readonly string customer_companyAAA_radiobtn = "//input[@type='radio'][following-sibling::text()[contains(., 'AAA')]]";
        public static readonly string customer_companyBBB_radiobtn = "//input[@type='radio'][following-sibling::text()[contains(., 'BBB')]]";
        public static readonly string role_dropdown = "//td/select[@name = 'RoleId']";
            public static readonly string salesoption_role_dropdown = "//td/select[@name = 'RoleId']/option[contains(.,'Sales')]";
            public static readonly string customeroption_role_dropdown = "//td/select[@name = 'RoleId']/option[contains(.,'Customer')]";
            public static readonly string adminoption_role_dropdown = "//td/select[@name = 'RoleId']/option[contains(.,'Admin')]";
        public static readonly string email_input = "//td/input[@name = 'Email']";
        public static readonly string cellphone_input = "//td/input[@name = 'Mobilephone']";

        public static readonly string save_btn = "//button[@class = 'btn btn-success']";
        public static readonly string close_btn = "//button[@class = 'btn btn-danger']";
    }
}

