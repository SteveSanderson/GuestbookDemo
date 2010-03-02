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