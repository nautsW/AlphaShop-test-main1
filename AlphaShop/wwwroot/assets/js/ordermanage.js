document.addEventListener('DOMContentLoaded',function (){

    modal1.style.display = "none";

    const showBtn = document.getElementsByName("act_btn");

    for (const btn of showBtn) { 
    btn.addEventListener('click', function () {
      modal1.style.display = 'flex';
    });
  }
    const closeModalBtn = document.getElementById('closeModalBtn');

    closeModalBtn.addEventListener('click', function () {
        modal1.style.display = "none";
    });
})