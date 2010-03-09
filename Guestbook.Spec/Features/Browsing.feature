Feature: Browsing
	In order to see who's been on the site
	As a user
	I want to be able to view the list of posts

Scenario: Navigation to homepage
	When I navigate to /Guestbook
	Then I should be on the guestbook page

Scenario: Viewing existing entries
	Given I am on the guestbook page
	Then I should see a list of guestbook entries
		And guestbook entries have an author
		And guestbook entries have a posted date
		And guestbook entries have a comment

Scenario: Most recent entries are displayed first
	Given we have the following existing entries
		| Name      | Comment      | Posted date       |
		| Mr. A     | I like A     | 2008-10-01 09:20  |
		| Mrs. B    | I like B     | 2010-03-05 02:15  |
		| Dr. C     | I like C     | 2010-02-20 12:21  |
      And I am on the guestbook page
    Then the guestbook entries includes the following, in this order
		| Name      | Comment      | Posted date       |
		| Mrs. B    | I like B     | 2010-03-05 02:15  |
		| Dr. C     | I like C     | 2010-02-20 12:21  |
		| Mr. A     | I like A     | 2008-10-01 09:20  |
