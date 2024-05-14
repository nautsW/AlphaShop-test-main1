
document.addEventListener('DOMContentLoaded', function () {
    const closeButton = document.getElementById('ev');
    const closeButton1 = document.getElementById('ev1');
    const closeButton2 = document.getElementById('ev2');
    const closeButton3 = document.getElementById('ev3');
    const closeButton4 = document.getElementById('ev4');
    btncp.addEventListener('click', function () {
        modal1.style.display = 'flex';
    });
    closeButton.addEventListener('click', function () {
        modal1.style.display = 'none';
    });

    btncs.addEventListener('click', function () {
        modal2.style.display = 'flex';
    });
    closeButton1.addEventListener('click', function () {
        modal2.style.display = 'none';
    });
    btnce.addEventListener('click', function () {
        modal3.style.display = 'flex';
    });
    closeButton2.addEventListener('click', function () {
        modal3.style.display = 'none';
    });

    btncx.addEventListener('click', function () {
        modal4.style.display = 'flex';
    });
    closeButton3.addEventListener('click', function () {
        modal4.style.display = 'none';
    });
    btnadd.addEventListener('click', function () {
        modal5.style.display = 'flex';
    });
    closeButton4.addEventListener('click', function () {
        modal5.style.display = 'none';
    });
});


