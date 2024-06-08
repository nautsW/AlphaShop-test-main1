OpenComment = function (id, userid) {
    const commentForm = document.getElementById('modal1');
    commentForm.style.display = 'unset';
    document.getElementById('ctr_cmt_js').value = userid;
    document.getElementById('prd_cmt_js').value = id;

    const cancel = document.getElementById('ev');
    cancel.addEventListener('click', function () {
        commentForm.style.display = 'none';
    });
}