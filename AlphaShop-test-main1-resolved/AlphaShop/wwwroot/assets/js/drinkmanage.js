document.addEventListener('DOMContentLoaded', function () {
    modal1.style.display = "none";
    modal2.style.display = "none";
    modal3.style.display = "none";
    modal4.style.display = "none";
    modal5.style.display = "none";

    const show_btn1 = document.getElementById("add_ctg");
    const show_btn2 = document.getElementById("add_prd");

    //const show_btn3 = document.getElementById("drink_btn");

    const show_btn4 = document.getElementById("upd_btn");
    const show_btn5 = document.getElementById("del_btn");
    const close_btn1 = document.getElementById('ev1');
    const close_btn2 = document.getElementById('btn-Cancel');
    const close_btn3 = document.getElementById('close_btn');
    const close_btn4 = document.getElementById('btn-Cancel_2');
    const close_btn5 = document.getElementById('btn-Cancel_3');

    show_btn1.addEventListener('click', function () {
        modal1.style.display = "flex";
    });
    show_btn2.addEventListener('click', function () {
        modal2.style.display = "flex";
    });

    //show_btn3.addEventListener('click', function () {
    //    modal3.style.display = "flex";
    //});

    show_btn4.addEventListener('click', function () {
        modal4.style.display = "flex";
    });
    show_btn5.addEventListener('click', function () {
        modal5.style.display = "flex";
    });


    close_btn1.addEventListener('click', function () {
        modal1.style.display = 'none';
    });
    close_btn2.addEventListener('click', function () {
        modal2.style.display = 'none';
    });
    close_btn3.addEventListener('click', function () {
        modal3.style.display = 'none';
    });
    close_btn4.addEventListener('click', function () {
        modal4.style.display = 'none';
    });
    close_btn5.addEventListener('click', function () {
        modal5.style.display = 'none';
    });
})

Info = function (id, name, price, status, image, desc) {
    try {
        document.getElementById("st__value_0").textContent = id;
        document.getElementById("st__value_1").textContent = name;
        document.getElementById("st__value_2").textContent = price;
        document.getElementById("image-perform").src = image;
        document.getElementById("image-perform-1").src = image;

        if (status === 1) {
            document.getElementById("st__value_3").textContent = "Còn hàng";
            document.forms["_DrinkEdit"]["rd__3"].checked = true;
        }
        else {
            document.getElementById("st__value_3").textContent = "Hết hàng";
            document.forms["_DrinkEdit"]["rd__4"].checked = true;

        }
        modal3.style.display = "flex";
        document.getElementById("change__0").value = id;
        document.getElementById("change__1").value = name;
        document.getElementById("change__2").value = price;
        document.getElementById("change__4").value = id;

        //if (status == 1) {
        //    document.getElementById('rd__3').checked = true;
        //}
        //else {
        //    document.getElementById('rd__4').checked = true;
        //}
        document.getElementById("desc-input").textContent = desc;
    }
    catch (err) {
        console.log(err);
    }
    

}
