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
                    return '<div style="display:flex; justify-content:center; gap:5px;"><button type="button" id="editDeptButton" class="btn btn-warning" data-tooltip="tooltip" data-placement="top" title="Edit Data" onclick="editDepartment(\'' + row.dept_ID + '\')"> <i class="fas fa-pencil-alt"></i></button > ' +
                        ' | <button type="button" id="deleteDeptButton" class="btn btn-danger" data-tooltip="tooltip" data-placement="top" title="Delete Data" onclick="deleteDepartment(\'' + row.dept_ID + '\')"><i class="fas fa-trash"></i></button></div > ';
                }
            }
        ]
    });
});

// menampilkan tooltip
$('[data-tooltip="tooltip"]').tooltip({ trigger: "hover" });

// menampilkan tooltip (ajax)
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
            Swal.fire({
                icon: "success",
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            });
            $('#DepartmentTable').DataTable().ajax.reload();
        } else {
            //alert(result.message);
            Swal.fire(result.message);
        }
    });
}

// fungsi untuk memperbaharui data melalui api menggunakan ajax
function editDepartment(id) {
    // sembunyikan tombol addDept
    document.getElementById('addDept').style.display = 'none';
    document.getElementById('editDept').style.display = 'block';

    // edit logic
    $.ajax({
        url: "https://localhost:7121/api/Department/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //success: function (result) {
        //    console.log(result);
        //},
    })
        .then((result) => {
            var obj = result.data;
            $('#deptID').val(obj.dept_ID);
            $('#deptInit').val(obj.dept_Initial);
            $('#deptName').val(obj.dept_Name);

            var btn = $('#editDept');
            btn.attr('onclick', 'edit(\'' + id + '\')');

            $('#exampleModal').modal('show');
    });
}

function edit(id) {
    var department = new Object();
    //department.dept_ID = $('#deptID').val();
    department.dept_Initial = $('#deptInit').val();
    department.dept_Name = $('#deptName').val();
    $.ajax({
        url: "https://localhost:7121/api/Department/" + id,
        type: "PUT",
        data: JSON.stringify(department),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            Swal.fire({
                icon: "success",
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            });
            $('#DepartmentTable').DataTable().ajax.reload();
        } else {
            //alert(result.message);
            Swal.fire(result.message);
        }
    });
}

// fungsi untuk menghapus data melalui api menggunakan ajax
function deleteDepartment(id) {
    //var confirmation = confirm("DeptID: " + id + ", Apakah anda yakin ingin menghapus data ini?");
    Swal.fire({
        title: "Hapus Data?",
        text: "Yakin Ingin Menghapus Data?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Delete"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7121/api/Department/" + id,
                type: "DELETE",
                contentType: "application/json; charset=utf-8",
            }).then((result) => {
                if (result.status == 200) {
                    //alert(result.message);
                    Swal.fire({
                        title: "Terhapus",
                        text: "Data Berhasil Dihapus",
                        icon: "success",
                    });
                    $('#DepartmentTable').DataTable().ajax.reload();
                } else {
                    alert(result.message);
                }
            });
            
        }
    });
}

// reset form setiap kali modal dibuka
$('#exampleModal').on('hidden.bs.modal', function () {
    $("#deptForm").trigger('reset');
});

// focus ke deptInit saat modal dibuka
$('#exampleModal').on('shown.bs.modal', function () {
    $("#deptInit").focus();
});

// saat tombol dengan id 'modalButton' diklik, tombol yang muncul pada modal hanya tombol tambah department (id 'addDept') saja
document.getElementById('modalButton').addEventListener('click', function () {
    document.getElementById('addDept').style.display = 'block';
    document.getElementById('editDept').style.display = 'none';
});
