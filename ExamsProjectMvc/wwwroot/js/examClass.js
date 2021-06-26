class ExamJs {
    constructor(questions) {
        this.questions = questions;
        this.currentIndex = 0;
        this.userAnswers = [];
        for (var i = 0; i < questions.length; i++) {
            this.userAnswers.push(questions[i].UserAnswer);
        }
    }


    getCurrentQuestion() {
        return this.questions[this.currentIndex];
    }
    getQuestionByIndex = function (index) {
        if (index >= 0 && index < questions.length)
            return this.question[index];
    }
    goNext = function () {
        this.currentIndex++;
    }
    goPrev = function () {
        if (this.currentIndex > 0)
            this.currentIndex--;
    }

    rearangeOrder = function () {
        this.questions.forEach(question => {
            question.Answers.sort(() => Math.random() - 0.5);
        });
        this.questions.sort(() => Math.random() - 0.5);
    }

}