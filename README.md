# PatientManagementApp_2024

<h1> MindTrack: 
manage your psychology practice with ease </h1>

<h2>Introduction</h2>
This is a web application for managing patient information for a small-scale psychology practice. Currently, it is designed for internal use only, meaning the only users are the practitioners within the organization. 


<h2>Users</h2>
Each practitioner in the company has their own account, to ensure privacy and confidentiality. The patient’s information is only accessible via their own psychologist’s account. 

There is an administrator for the web app, who can see and manage all of the information for every practitioner, including the deleted patients, practitioners, and appointments. The admin is the only account, who can restore soft-deleted entities. 

<h2>Functionality</h2>
A psychologist can:
see, add, edit, and soft delete patient information
create notes for the patient after an appointment
create/edit/delete an appointment 
have an overview of upcoming appointments 

Test accounts with seeded info:
- admin: 
  - username: admin@admin.com
  - password: admin123

- Practitioner 1:
  - username: practitioner1@example.com
  - password: Password@123

- Practitioner 2:
  - username: practitioner2@example.com
  - password: Password@123

Feel free to use any of the created accounts or create your own account as well.


<h2>Future scalability</h2>

- migrate the file storage from the DB to a cloud-based storage place

- introduce patient profiles
    -  patients can log in via the website of the medical practice
    -  Fill out patient information and medical history
    -  Upload documents
    -  Provide feedback for the practitioners

- Appointments 
    -  implement online appointment booking 
    -  book/change/cancel an appointment
  
- add mailing system 
    -  Email confirmation when registering 
    -  Send automatic emails to patients for booked appointments
    -  Send billing reminders and payment confirmations

- add online payment option
