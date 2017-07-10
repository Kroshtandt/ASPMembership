function ItemViewModel() {
    var self = this;
    self.UserId = window.userId;
    self.Roles = ko.observableArray();
    self.UserRoles = ko.observableArray();

    self.GetAllRoles = () => {
        $.ajax({
            url: '/rolesApi/getAllRoles',
            type: "GET",
            success: function (data) {
                self.Roles(data);
            }
        });
    }

    self.GetUserRoles = () => {
        $.ajax({
            url: '/rolesApi/GetRolesByUserId/' + self.UserId,
            type: "GET",
            success: function (data) {
                self.UserRoles(data);
                self.GetAllRoles();
            }
        });
    }
    self.GetUserRoles();


    self.attachRoleToUser = (data) => {
        var userRoles = self.UserRoles();
        for (var i = 0; i < userRoles.length; i++) {
            if (userRoles[i].Id == data.Id) {
                alert('User already has this role');
                return;
            }
        }

        var oldRoles = self.UserRoles();
        oldRoles.push(data)
        self.UserRoles(oldRoles);
        $.ajax({
            url: '/rolesApi/attachRoleToUser/' + self.UserId + '/' + data.Name,
            type: 'POST'
        });
    }

    self.detachRoleFromUser = (data) => {
        var userRoles = self.UserRoles();
        for (var i = 0; i < userRoles.length; i++) {
            if (userRoles[i].Id == data.Id) {
                userRoles.splice(i, 1);
            }
        }
        self.UserRoles(userRoles);
        $.ajax({
            url: '/rolesApi/detachRoleFromUser/' + self.UserId + '/' + data.Name,
            type: 'POST'
        });
    }
}

var viewModel = new ItemViewModel();

ko.applyBindings(viewModel);