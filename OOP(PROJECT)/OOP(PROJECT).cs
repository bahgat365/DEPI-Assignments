using System;

namespace ExaminationSystem
{
    public class Answer : ICloneable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer() : this(0, "")
        {
        }

        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }

        public object Clone()
        {
            return new Answer(AnswerId, AnswerText);
        }

        public override string ToString()
        {
            return $"{AnswerId}. {AnswerText}";
        }
    }

    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] AnswerList { get; set; }
        public Answer RightAnswer { get; set; }

        protected Question() : this("", "", 0)
        {
        }

        protected Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
            AnswerList = Array.Empty<Answer>();
        }

        public abstract void ShowQuestion();

        public int CompareTo(Question other)
        {
            if (other == null)
                return 1;

            return Mark.CompareTo(other.Mark);
        }

        public abstract object Clone();

        public override string ToString()
        {
            return $"{Header}: {Body} - Mark: {Mark}";
        }
    }

    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion() : this("", "", 0)
        {
        }

        public TrueFalseQuestion(string header, string body, int mark)
            : base(header, body, mark)
        {
            AnswerList = new[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
        }

        public override void ShowQuestion()
        {
            Console.WriteLine($"{Header}");
            Console.WriteLine($"{Body}");

            foreach (Answer answer in AnswerList)
                Console.WriteLine(answer);

            Console.WriteLine($"Right Answer: {RightAnswer}");
            Console.WriteLine();
        }

        public override object Clone()
        {
            TrueFalseQuestion question = new TrueFalseQuestion(Header, Body, Mark);
            question.AnswerList = new Answer[AnswerList.Length];

            for (int i = 0; i < AnswerList.Length; i++)
                question.AnswerList[i] = (Answer)AnswerList[i].Clone();

            if (RightAnswer != null)
                question.RightAnswer = (Answer)RightAnswer.Clone();

            return question;
        }
    }

    public class MCQQuestion : Question
    {
        public MCQQuestion() : this("", "", 0)
        {
        }

        public MCQQuestion(string header, string body, int mark)
            : base(header, body, mark)
        {
        }

        public override void ShowQuestion()
        {
            Console.WriteLine($"{Header}");
            Console.WriteLine($"{Body}");

            foreach (Answer answer in AnswerList)
                Console.WriteLine(answer);

            Console.WriteLine($"Right Answer: {RightAnswer}");
            Console.WriteLine();
        }

        public override object Clone()
        {
            MCQQuestion question = new MCQQuestion(Header, Body, Mark);
            question.AnswerList = new Answer[AnswerList.Length];

            for (int i = 0; i < AnswerList.Length; i++)
                question.AnswerList[i] = (Answer)AnswerList[i].Clone();

            if (RightAnswer != null)
                question.RightAnswer = (Answer)RightAnswer.Clone();

            return question;
        }
    }

    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public Question[] Questions { get; set; }
        public Subject Subject { get; set; }

        protected Exam() : this(0, 0)
        {
        }

        protected Exam(int time, int numberOfQuestions)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Questions = new Question[numberOfQuestions];
        }

        public abstract void ShowExam();

        public int CompareTo(Exam other)
        {
            if (other == null)
                return 1;

            return NumberOfQuestions.CompareTo(other.NumberOfQuestions);
        }

        public abstract object Clone();

        public override string ToString()
        {
            return $"Exam Time: {Time} minutes, Questions: {NumberOfQuestions}";
        }
    }

    public class PracticalExam : Exam
    {
        public PracticalExam() : this(0, 0)
        {
        }

        public PracticalExam(int time, int numberOfQuestions)
            : base(time, numberOfQuestions)
        {
        }

        public override void ShowExam()
        {
            Console.WriteLine("Practical Exam");
            Console.WriteLine($"Time: {Time} minutes");
            Console.WriteLine();

            for (int i = 0; i < Questions.Length; i++)
            {
                Console.WriteLine($"Question {i + 1}");
                Questions[i].ShowQuestion();
            }

            Console.WriteLine("The correct answers are shown above.");
        }

        public override object Clone()
        {
            PracticalExam exam = new PracticalExam(Time, NumberOfQuestions);

            for (int i = 0; i < Questions.Length; i++)
                exam.Questions[i] = (Question)Questions[i].Clone();

            exam.Subject = Subject;

            return exam;
        }
    }

    public class FinalExam : Exam
    {
        public FinalExam() : this(0, 0)
        {
        }

        public FinalExam(int time, int numberOfQuestions)
            : base(time, numberOfQuestions)
        {
        }

        public override void ShowExam()
        {
            Console.WriteLine("Final Exam");
            Console.WriteLine($"Time: {Time} minutes");
            Console.WriteLine();

            int grade = 0;
            int totalMark = 0;

            for (int i = 0; i < Questions.Length; i++)
            {
                Question question = Questions[i];

                Console.WriteLine($"Question {i + 1}");
                Console.WriteLine(question.Header);
                Console.WriteLine(question.Body);

                foreach (Answer answer in question.AnswerList)
                    Console.WriteLine(answer);

                Console.WriteLine($"Right Answer: {question.RightAnswer}");
                Console.WriteLine($"Mark: {question.Mark}");
                Console.WriteLine();

                grade += question.Mark;
                totalMark += question.Mark;
            }

            Console.WriteLine($"Grade: {grade} / {totalMark}");
        }

        public override object Clone()
        {
            FinalExam exam = new FinalExam(Time, NumberOfQuestions);

            for (int i = 0; i < Questions.Length; i++)
                exam.Questions[i] = (Question)Questions[i].Clone();

            exam.Subject = Subject;

            return exam;
        }
    }

    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }

        public Subject() : this(0, "")
        {
        }

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }

        public void CreateExam(Exam exam)
        {
            Exam = exam;
            Exam.Subject = this;
        }

        public override string ToString()
        {
            return $"{SubjectId} - {SubjectName}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject(1, "Object Oriented Programming");

            FinalExam finalExam = new FinalExam(60, 2);

            TrueFalseQuestion q1 = new TrueFalseQuestion(
                "Question 1",
                "C# supports inheritance.",
                5
            );
            q1.RightAnswer = q1.AnswerList[0];

            MCQQuestion q2 = new MCQQuestion(
                "Question 2",
                "Which keyword is used to inherit from a class in C#?",
                5
            );
            q2.AnswerList = new[]
            {
                new Answer(1, "extends"),
                new Answer(2, "inherits"),
                new Answer(3, ":"),
                new Answer(4, "base")
            };
            q2.RightAnswer = q2.AnswerList[2];

            finalExam.Questions[0] = q1;
            finalExam.Questions[1] = q2;

            subject.CreateExam(finalExam);

            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine();

            subject.Exam.ShowExam();

            Console.WriteLine();
            Console.WriteLine("Clone Test:");

            FinalExam clonedExam = (FinalExam)subject.Exam.Clone();
            Console.WriteLine(clonedExam);

            Console.WriteLine();
            Console.WriteLine("Comparison Test:");

            PracticalExam practicalExam = new PracticalExam(45, 1);
            Console.WriteLine(
                finalExam.CompareTo(practicalExam) > 0
                    ? "Final Exam has more questions."
                    : "Practical Exam has more or equal questions."
            );
        }
    }
}
