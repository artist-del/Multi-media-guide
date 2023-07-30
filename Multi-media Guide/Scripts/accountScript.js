function edit(data) {
    $.ajax({
        type: 'get',
        url: '/User/GetId?id=' + data,
        success: (e) => {
            var obj = JSON.parse(e);
            
            $('#userId').val(obj.Id);
            $('#tbl_name').val(obj.f_name);
            $('#tbl_middlename').val(obj.m_name);
            $('#tbl_lastName').val(obj.l_name);
            $('#tbl_email').val(obj.a_email_add);
            $('#a_contact_num').val(obj.a_contact_num)
            $('#tbl_address').val(obj.a_address);
            $('#tbl_username').val(obj.a_user);
            $('#tbl_password').val(obj.a_pass);
        }
    })
}

$('#saveBtn').click(() => {
    var data = $('#editForm').serialize();

    $.ajax({
        type: 'post',
        url: '/User/edit',
        data: data,
        success: (e) => {
            if (e.status === true) {
                Swal.fire('Information', 'Successfully Save', 'success').then(() => {
                    window.location.href = "/User/Index";
                })
            }
            else {
                Swal.fire('Information', 'Something wrong!', 'warning');
            }
        }
    })
})

function delData(data){
    $('#delId').val(data);
}

$('#btnDel').click(() => {
    let data = $('#delId').val();

    $.ajax({
        type: 'post',
        url: '/User/delete_user?id=' + data,
        success: (e) => {
            if (e.status == true) {
                Swal.fire('Information', 'Successfully Deleted!', 'success').then(() => {
                    window.location.href = '/User/Index';
                })
            } else {
                Swal.fire('Warning', 'Something Wrong!', 'warning');
            }
        }
        
    })
})