$(document).ready(function () {
    $('#DepartmentTable').DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "ajax": {
            url: "https://localhost:7121/api/Department",
            type: "GET",
            datatype: "json",
            "datasrc": "data",
            //success: function (result) {
            //    console.log(result);
            //},
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {"data": "dept_ID",},
            { "data": "dept_Initial"},
            { "data": "dept_Name" },
            {
                "render": function (data, type, row) {
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editDepartment(' + row.dept_ID + ')" > <i class="fas fa-pencil-alt"></i></button > ' +
                    ' | <button type="button" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteDepartment(' + row.dept_ID + ')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

$('[data-tooltip="tooltip"]').tooltip({trigger: "hover"});

$(document).ajaxComplete(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

function Save() {
    var department = new Object();
    department.dept_Initial = $('#deptInit').val();
    department.dept_Name = $('#deptName').val();
    $.ajax({
        url: "https://localhost:7121/api/Department/",
        type: "POST",
        data: JSON.stringify(department),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            alert(result.message);
            $('#DepartmentTable').DataTable().ajax.reload();
        } else {
            alert(result.message);
        }
    });
}
