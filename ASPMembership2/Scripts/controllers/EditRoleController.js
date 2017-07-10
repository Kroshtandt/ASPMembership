function ItemViewModel() {
    var self = this;
    self.RoleId = window.roleId;
    self.RoleEntity = ko.observable({});
    self.Methods = ko.observableArray();

    if (self.RoleId) {
        $.ajax({
            url: '/rolesapi/getRoleById/' + self.RoleId + '/1',
            type: "GET",
            success: function (data) {
                self.RoleEntity(data);
                self.getAvailableAppMethods();
            }
        });
    }

    self.updateRole = () => {

        $.ajax({
            url: '/rolesapi/saverole/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(self.RoleEntity()),
            success: function (data) {
                alert('Saved!');
                window.location.href = '/Roles/RolesList';
            }
        });
    }

    self.getAvailableAppMethods = () => {
        $.ajax({
            url: '/appmethodsapi/getApplicationMethodsList',
            type: "GET",
            success: function (data) {
                self.Methods(data);
            }
        });
    }

    self.attachMethodToRole = function (data) {
        var role = self.RoleEntity();
        for (var i = 0; i < role.Methods.length; i++) {

            var methodId = role.Methods[i].MethodId;
            if (data.MethodId === methodId) {
                alert('This method already attached to the role')
                return;
            }
        }
        role.Methods.push(data);
        self.RoleEntity(role);

        $.ajax({
            url: '/appMethodsApi/attachMethodToRole/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(
                {
                    "RoleId": role.Id,
                    "MethodId": data.MethodId
                }),
            success: function (data) {
                alert('Saved!');
            }
        });
    }

    self.detachMethodToRole = function (data) {
        if (confirm('Are you sure to remove method from role?')) {
            var role = self.RoleEntity();
            for (var i = 0; i < role.Methods.length; i++) {

                var methodId = role.Methods[i].MethodId;
                if (data.MethodId === methodId) {
                    role.Methods.splice(i, 1);
                    break;
                }
            }
            self.RoleEntity(role);

            $.ajax({
                url: '/appMethodsApi/detachMethodFromRole/',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(
                    {
                        "RoleId": role.Id,
                        "MethodId": data.MethodId
                    }),
                success: function (data) {
                }
            });
        }

    }
}

var viewModel = new ItemViewModel();

ko.applyBindings(viewModel);