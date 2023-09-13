using System;

namespace Utils
{
    public static class JS
    {
        public static string confirm(string content)
        {
            return $@"<script>
                function confirmAction() {{
                    var result = confirm('{content}');
                    if (result) {{
                        return true;
                    }}
                    else {{
                        return false;
                    }}
                }}
            </script>";
        }

        public static string confirmDelete()
        {
            return @"<script>
                    function confirmDelete() {
                        var result = confirm(""Are you sure you want to delete this customer?"");
                        if (result) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                </script>";
        }

        public static string confirmUnassign()
        {
            return @"<script>
                    function confirmUnassgin() {
                        var result = confirm(""Are you sure you want to unassign for this workshift?"");
                        if (result) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                </script>";
        }

        public static string confirmAssign()
        {
            return @"<script>
                    function confirmAssgin() {
                        var result = confirm(""Are you sure you want to Assign for this workshift?"");
                        if (result) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                </script>";
        }
    }
}
