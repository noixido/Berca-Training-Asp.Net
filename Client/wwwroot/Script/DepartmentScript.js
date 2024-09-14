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
            { "data": "dept_ID", },
            { "data": "dept_Initial" },
            { "data": "dept_Name" },
            {
                "render": function (data, type, row) {
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" id="editDeptButton" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editDepartment(\'' + row.dept_ID + '\')" data-toggle="modal" data-target="#exampleModal"> <i class="fas fa-pencil-alt"></i></button > ' +
                        ' | <button type="button" id="deleteDeptButton" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteDepartment(\'' + row.dept_ID + '\')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

// menampilkan tooltip di luar ajax
$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

// menampilkan tooltip di dalam ajax
$(document).ajaxComplete(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: "hover",
    });
});

// fungsi untuk menyimpan data melalui api menggunakan ajax
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

// fungsi untuk memperbaharui data melalui api menggunakan ajax
function editDepartment(id) {

}

// fungsi untuk menghapus data melalui api menggunakan ajax
function deleteDepartment(id) {
    var confirmation = confirm("Apakah anda yaking ingin menghapus data ini?");
    if (confirmation) {

        $.ajax({
            url: "https://localhost:7121/api/Department/" + id,
            type: "DELETE",
            contentType: "application/json; charset=utf-8",
        }).then((result) => {
            if (result.status == 200) {
                alert(result.message);
                $('#DepartmentTable').DataTable().ajax.reload();
            } else {
                alert(result.message);
            }
        });
    } else {
        alert("Penghapusan data dibatalkan");
    }
}

// saat tombol dengan id 'modalButton' diklik, tombol yang muncul pada modal hanya tombol tambah department (id 'addDept') saja
document.getElementById('modalButton').addEventListener('click', () => {
    document.getElementById('addDept').style.display = 'block';
    document.getElementById('editDept').style.display = 'none';
});
// saat tombol dengan id 'editDeptButton' diklik, tombol yang muncul pada modal hanya tombol edit department (id 'editDept') saja (untuk ajax)
$(document).ajaxComplete(function () {
    document.getElementById('editDeptButton').addEventListener('click', () => {
        document.getElementById('addDept').style.display = 'none';
        document.getElementById('editDept').style.display = 'block';
    });
});