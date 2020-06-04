using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http.Headers;
using System.Resources;
using System.Reflection.PortableExecutable;
using System.Security;

namespace Class_training
{

	class Person : IDateAndCopy, IComparable, IComparer<Person>
	{

		// ------------------------------------Поля класса-----------------------------------------
		protected string name;
		protected string surname;

		public DateTime Date { get; set; }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Surname
		{
			get { return surname; }
			set { surname = value; }
		}

		public int ChangeBornDate
		{
			get { return Date.Year; }
			set
			{
				DateTime bornYear = new DateTime(value, Date.Month, Date.Day);
				Date = bornYear;
			}
		}


		// Конструкторы класса
		public Person(string name, string surname, System.DateTime bornDate)
		{
			this.name = name;
			this.surname = surname;
			this.Date = bornDate;
		}

		public Person()
		{
			name = "Name";
			surname = "Surname";
			Date = new DateTime(2000, 1, 1);
		}

		// Методы
		public override string ToString()
		{
			return string.Format($"{name} -  имя \n" +
				$"{surname} -  фамилия \n" +
				$"{Date} - датa народження \n");
		}

		public virtual string ToShortString()
		{
			return name + " " + surname;
		}




		public static bool operator ==(Person exam, Person exam2)
		{
			if (exam.Equals(exam2) == true)
				return true;
			else
				return false;
		}

		public static bool operator !=(Person exam, Person exam2)
		{
			if (exam.Equals(exam2) != true)
				return true;
			else
				return false;
		}

		public override bool Equals(object obj)
		{
			Person example = (Person)obj;
			if (Object.ReferenceEquals(example, null))
			{
				return false;
			}
			if (this.name == example.name && this.surname == example.surname && this.Date == example.Date)
			{
				return true;
			}
			else
				return false;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public virtual object DeepCopy()
		{
			return new Person(this.name, this.surname, this.Date);
		}

		int IComparable.CompareTo(object obj)
		{
			Person temp = obj as Person;

			if(temp != null)
			{
				return String.Compare(this.Surname, temp.Surname);
			}
			else
			{
				throw new ArgumentException("is not a Person");
			}
		}

		public int Compare(Person x, Person y)
		{
			return DateTime.Compare(x.Date, y.Date);
		}
	}

	class ExtraDoctor : IComparer<Doctor>
	{
		public int Compare(Doctor x, Doctor y)
		{
			return x.WorkTime.CompareTo(y.WorkTime);
		}
	}

	class DoctorCollection
	{
		private List<Doctor> doctorList = new List<Doctor>();
		public void AddDefaults()
		{
			doctorList.Add(new Doctor(new Person("Person1", "Aurname1", new DateTime(2006, 12, 1)), "surgery", Category.First, 100 ));
			doctorList.Add(new Doctor(new Person("Person2", "Durname2", new DateTime(2001, 12, 1)), "surgery", Category.First, 400 ));
			doctorList.Add(new Doctor(new Person("Person3", "Burname3", new DateTime(2001, 12, 1)), "surgery", Category.First, 500 ));
			doctorList.Add(new Doctor(new Person("Person4", "Surname4", new DateTime(2005, 12, 1)), "surgery", Category.First, 200 ));
			doctorList.Add(new Doctor(new Person("Person5", "Curname5", new DateTime(2002, 12, 1)), "surgery", Category.First, 300 ));
		}

		void AddDoctors(params Doctor[] doctors)
		{
			doctorList.AddRange(doctors);
		}


		public override string ToString()
		{
			string doctorInfo = "";
			foreach (Doctor p in doctorList)
			{
				doctorInfo += p.ToString();
			}

			return doctorInfo;
		}

		public string ToShortString()
		{
			string doctorInfo = "";
			foreach (Doctor p in doctorList)
			{
				doctorInfo += p.ToShortString();
			}
			return doctorInfo;
		}

		public void SurnameSort()
		{
			doctorList.Sort();
		}

		public void DateSort()
		{
			doctorList.Sort(new Person());
		}

		public void WorkTimeSort()
		{
			doctorList.Sort(new ExtraDoctor());
		}
	}

	class TestCollection
	{
		List<Person> personList = new List<Person>();
		List<string> stringList = new List<string>();

		Dictionary<Person, Doctor> personDoctorDic = new Dictionary<Person, Doctor>();
		Dictionary<string, Doctor> stringDoctorDic = new Dictionary<string, Doctor>();
	
		static Doctor GenerateDoctor(int number)
		{
			return new Doctor(new Person("Name" + number, "Surname", DateTime.Now), "nurse", Category.Second, 50);
		}


		public TestCollection(int count)
		{
			personList = new List<Person>();
			stringList = new List<string>();
			personDoctorDic = new Dictionary<Person, Doctor>();
			stringDoctorDic = new Dictionary<string, Doctor>();

			for (int i = 0; i < count; i++)
			{
				Doctor newDoctor = GenerateDoctor(i);
				Person newPerson = newDoctor.PersonalInformation;
				personList.Add(newPerson);
				stringList.Add(newPerson.ToString());
				personDoctorDic.Add(newPerson, newDoctor);
				stringDoctorDic.Add(newPerson.ToString(), newDoctor);
			}
		}

		public void Time(int indexOfElem)
		{
			Person toFindTimePerson = null;
			Doctor toFindTimeDoctor = null;
			string toFindTimeString = null;

			if (personList.Count <= indexOfElem || indexOfElem < 0)
			{
				toFindTimePerson = new Person();
				toFindTimeString = " string ";
				toFindTimeDoctor = new Doctor();
			}
			else
			{
				toFindTimePerson = personList[indexOfElem];
				toFindTimeString = stringList[indexOfElem];
				toFindTimeDoctor = personDoctorDic[toFindTimePerson];
			}
			


			DateTime start = DateTime.Now;
			Console.WriteLine(personList.Contains(toFindTimePerson));
			Console.WriteLine($"Milliseconds for searching in List<Person> {(DateTime.Now - start).TotalMilliseconds}");
			
			start = DateTime.Now;
			Console.WriteLine(stringList.Contains(toFindTimeString));
			Console.WriteLine($"Milliseconds for searching in List<String> {(DateTime.Now - start).TotalMilliseconds}");
			
			
			
			start = DateTime.Now;
			Console.WriteLine(personDoctorDic.ContainsKey(toFindTimePerson));
			Console.WriteLine($"Milliseconds for searching in Dictionary<Person, Doctor> by key {(DateTime.Now - start).TotalMilliseconds}");
			
			start = DateTime.Now;
			Console.WriteLine(personDoctorDic.ContainsValue(toFindTimeDoctor));
			Console.WriteLine($"Milliseconds for searching in Dictionary<Person, Doctor> by value {(DateTime.Now - start).TotalMilliseconds}");


			start = DateTime.Now;
			Console.WriteLine(stringDoctorDic.ContainsKey(toFindTimeString));
			Console.WriteLine($"Milliseconds for searching in Dictionary<string, Doctor> by key {(DateTime.Now - start).TotalMilliseconds}");

			start = DateTime.Now;
			Console.WriteLine(stringDoctorDic.ContainsValue(toFindTimeDoctor));
			Console.WriteLine($"Milliseconds for searching in Dictionary<string, Doctor> by value {(DateTime.Now - start).TotalMilliseconds}");

		}
	}







	class Patient : Person
	{
		private string diagnos;
		private DateTime timeOfEntering;

		public string Diagnos
		{
			get { return diagnos; }
			set { diagnos = value; }
		}

		public DateTime TimeOfEntering
		{
			get { return timeOfEntering; }
			set { timeOfEntering = value; }
		}

		public Patient(Person information, string diagnos, DateTime timeOfEntering)
		{
			this.name = information.Name;
			this.surname = information.Surname;
			this.Date = information.Date;
			this.diagnos = diagnos;
			this.timeOfEntering = timeOfEntering;
		}

		public Patient()
		{
			diagnos = "healthy";
			timeOfEntering = new DateTime(2019, 12, 29);
		}
		public override string ToString()
		{
			return string.Format($"{Name} {Surname} \n" +
				$"{diagnos} -  диагноз \n" +
				$"{timeOfEntering} -  дата поступлеиния в стационар \n");
		}
	}

	class Diploma
	{

		public string orgName { get; set; }

		public string qualifications { get; set; }

		public DateTime diplomeDateTime { get; set; }



		public Diploma(string orgName, string qualifications, DateTime diplomeDateTime)
		{
			this.orgName = orgName;
			this.qualifications = qualifications;
			this.diplomeDateTime = diplomeDateTime;
		}

		public Diploma()
		{
			orgName = "DNU";
			qualifications = "default";
			diplomeDateTime = DateTime.UtcNow;
		}

		public override string ToString()
		{
			return string.Format($"{orgName} - назвaние организации, которая видала диплом (сертификат) \n" +
				$"{qualifications} -  полученая квалификация\n" +
				$"{diplomeDateTime} - датa получения диплома \n ");
		}
	}

	class Doctor : Person
	{
		private string specifacation;
		private Category category;
		private int workTime;
		private List<Diploma> diplomasList = new List<Diploma>();
		private List<Patient> patientList = new List<Patient>();


		// for 4 lab

		public Person PersonalInformation { get { return new Person(this.Name, this.Surname, this.Date); } set { this.name = value.Name; this.surname = value.Surname; this.Date = value.Date; } }

		// ///////////////////////////////////////
		public string Specifacation { get { return specifacation; } set { specifacation = value; } }

		public Category Category { get { return category; } set { category = value; } }

		public int WorkTime { get { return workTime; } set { workTime = value; } }

		public List<Diploma> DiplomasList { get { return diplomasList; } set { diplomasList = value; } }

		public List<Patient> PatientList { get { return patientList; } set { patientList = value; } }


		public Doctor()
		{
			specifacation = "default";
			category = Category.High;
			workTime = 99;
		}
		public Doctor(Person personalInformation, string specifacation, Category category, int workTime)
			: base(personalInformation.Name, personalInformation.Surname, personalInformation.Date)
		{
			this.specifacation = specifacation;
			this.category = category;
			this.workTime = workTime;
		}



		public Diploma getFirstDiploma
		{
			get
			{
				if (DiplomasList == null)
				{
					System.DateTime firstDiplom = diplomasList[0].diplomeDateTime;
					int iMin = 0;
					int i = 0;
					foreach (Diploma p in diplomasList)
					{
						if (firstDiplom > p.diplomeDateTime)
						{
							firstDiplom = p.diplomeDateTime;
							iMin = i;
						}
						i++;
					}
					return diplomasList[iMin];
				}
				else
					return null;

			}
		}

		public void AddDiplomas(params Diploma[] aboutDiploma)
		{
			diplomasList.AddRange(aboutDiploma);
		}

		public void AddPatients(params Patient[] aboutPatients)
		{
			patientList.AddRange(aboutPatients);
		}

		public override string ToString()
		{
			string diplomsInfo = "";
			foreach (Diploma p in diplomasList)
			{
				diplomsInfo += p.ToString();
			}

			string patientsInfo = "";
			foreach (Patient t in patientList)
			{
				patientsInfo += t.ToString();
			}

			return string.Format($"{PersonalInformation}\n" +
				$"{specifacation}, " +
				$"{category}, " +
				$"{workTime} \n" +
				$"{diplomsInfo}\n" +
				$"{patientsInfo}\n");
		}

		public override string ToShortString()
		{
			return string.Format($"{PersonalInformation}\n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n");
		}

		public object DeepCopy(Doctor doctor)
		{
			Doctor Copy = new Doctor()
			{
				PersonalInformation = this.PersonalInformation,
				Specifacation = this.Specifacation,
				Category = this.Category,
				workExperience = this.workExperience,
				PatientList = new List<Patient>(),
				DiplomasList = new List<Diploma>()
			};

			foreach (Patient p in PatientList)
			{
				Copy.PatientList.Add(new Patient(new Person(p.Name, p.Surname, p.Date), p.Diagnos, p.TimeOfEntering));
			}

			foreach (Diploma p in DiplomasList)
			{
				Copy.DiplomasList.Add(new Diploma(p.orgName, p.qualifications, p.diplomeDateTime));
			}


			return Copy;
		}



		public int workExperience
		{
			get
			{
				return workTime;
			}
			set
			{
				if (workTime < 0 || workTime > 100)
				{
					throw new Exception("Incorrect work time");
				}
				else
				{
					workTime = value;
				}
			}
		}


		public IEnumerator GetEnumerator()
		{
			return patientList.GetEnumerator();
		}

		public IEnumerable GetTodayPatient()
		{
			foreach (Patient p in patientList)
			{
				if (p.TimeOfEntering == DateTime.Today)
				{
					yield return p;
				}
			}
		}

		public IEnumerable GetPatientWith(string diagnos)
		{
			foreach (Patient p in patientList)
			{
				if (p.Diagnos == diagnos)
				{
					yield return p;
				}
			}
		}

	}



	class Program
	{
		static void Main()
		{
			DoctorCollection doctorCollection = new DoctorCollection();
			doctorCollection.AddDefaults();
			Console.WriteLine(doctorCollection);
			Console.WriteLine("/////////////////////////////////////////////");
			doctorCollection.SurnameSort();
			Console.WriteLine(doctorCollection);
			Console.WriteLine("/////////////////////////////////////////////");
			doctorCollection.DateSort();
			Console.WriteLine(doctorCollection);
			Console.WriteLine("/////////////////////////////////////////////");
			doctorCollection.WorkTimeSort();
			Console.WriteLine(doctorCollection);
			Console.WriteLine("/////////////////////////////////////////////");

			TestCollection testCollection = new TestCollection(1000000);

			Console.WriteLine("\nПоиск первого элемента:\n");
			testCollection.Time(0);

			Console.WriteLine("\nПоиск центрального элемента:\n");
			testCollection.Time(500000);

			Console.WriteLine("\nПоиск последнего элемента:\n");
			testCollection.Time(99999);

			Console.WriteLine("\nПоиск отсутствующего элемента:\n");
			testCollection.Time(1000000);
		}
	}

	public interface IDateAndCopy
	{
		object DeepCopy();
		DateTime Date { get; set; }
	}
	enum Category { High, First, Second }
}