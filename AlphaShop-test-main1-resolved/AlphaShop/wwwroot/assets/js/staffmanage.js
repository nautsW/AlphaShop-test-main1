document.addEventListener('DOMContentLoaded', function () {

    modal1.style.display = "none";
    modal2.style.display = "none";
    modal3.style.display = "none";


    /*const showBtn1 = document.getElementById('ctr_btn');*/
    const showBtn2 = document.getElementById('creat_acc');
    const showBtn3 = document.getElementById('edit_btn');
    const showBtn4 = document.getElementById('js_delete');
    //showBtn1.addEventListener('click', function () {
    //  modal1.style.display = 'flex';
    //});
    showBtn4.addEventListener('click', function () {
        modal5.style.display = 'flex';
    });
    showBtn2.addEventListener('click', function () {
        modal2.style.display = 'flex';
    });
    showBtn3.addEventListener('click', function () {
        modal3.style.display = 'flex';
    });

    const closeModalBtn1 = document.getElementById('close_btn');
    const closeModalBtn2 = document.getElementById('btn-Cancel');
    const closeModalBtn3 = document.getElementById('btn-Cancel__1');
    const closeModalBtn4 = document.getElementById('btn-Cancel_3');

    closeModalBtn1.addEventListener('click', function () {
        modal1.style.display = "none";
    });
    closeModalBtn2.addEventListener('click', function () {
        modal2.style.display = "none";
    });
    closeModalBtn3.addEventListener('click', function () {
        modal3.style.display = "none";
    });
    closeModalBtn4.addEventListener('click', function () {
        modal5.style.display = "none";
    });
})
Info = function (id, username, phonenumber, status, email, gender, access, address, password1, image) {
    try {

    const image_change = image.replace('assets', '/assets/').replace('img', 'img/').replace('pfp', 'pfp/');
        document.getElementById('staff_attr-id').textContent = id;
        document.getElementById('staff_attr-username').textContent = username;
        document.getElementById('staff_attr-pn').textContent = phonenumber;
        if (status == 1) {
            document.getElementById('staff_attr-status').textContent = "Bình thường";
        }
        else {
            document.getElementById('staff_attr-status').textContent = " Bị Khóa";
        }
        document.getElementById('staff_attr-email').textContent = email;
        if (gender === 'True') {
            document.getElementById('staff_attr-sex').textContent = 'Nam';
            document.forms["_EditAcc"]["rd__1"].checked = true;
        }
        else {
            document.getElementById('staff_attr-sex').textContent = 'Nữ';
            document.forms["_EditAcc"]["rd__2"].checked = true;

        }


        document.getElementById('staff_image').src = image_change;
        modal1.style.display = "flex";
        document.getElementById('staff_attr-addr').textContent = address;

        document.getElementById('staff_attr-id-1').value = id;
        document.getElementById('staff_attr-username-1').value = username;
        document.getElementById('staff_attr-pn-1').value = phonenumber;

        document.getElementById('staff_attr-addr-1').textContent = address;
        document.getElementById('staff_attr-email-1').value = email;
        document.getElementById('change__4').value = id;
        if (access === 1) {
            document.forms["_EditAcc"]["rd__3"].checked = true;

        }
        else if (access === 2) {
            document.forms["_EditAcc"]["rd__4"].checked = true;
        }
        const passwordInput = document.getElementById('staff_attr-pass');
        passwordInput.value = password1;
    }
    catch (err) {
        console.log(err);
    }



}