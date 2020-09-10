#include <iostream>
#include "BPS.h"


std::string path = "D:/Documentos/OneDrive/DESKTOP/BPSLib/";
//std::string path = "C:/Users/Panificadora Larissa/OneDrive/DESKTOP/BPSLib/";
std::string wf = "write_test";
std::string rf = "read_test.bps";

void rw_test();
void trim_test();
void split_test();
void section_tests();

int main() {
	rw_test();
	//trim_test();
	//split_test();
	//section_tests();

}

void rw_test() {
	BPS::File* file = BPS::read(path + rf);

	for(BPS::Section* s : file->findAll()) {
		std::cout << s->getName() << std::endl;
		for(BPS::Data* d : s->findAll()) {
			std::cout << d->getKey() + ":" + d->getValue() << std::endl;
		}
		std::cout << std::endl;
	}

	BPS::write(file, path + wf);
}

void trim_test() {
	std::cout << "<" + BPS::trim(" frase aqui  ") + ">\n";
	std::cout << "<" + BPS::trim(" palavra  ") + ">\n";
	std::cout << "<" + BPS::trim("     i  ") + ">\n";
	std::cout << "<" + BPS::trim("    ") + ">\n";
	std::cout << "<" + BPS::trim(" j         ") + ">\n";
	std::cout << "<" + BPS::trim("") + ">\n";
}

void split_test() {
	std::string str = "comi:pao:casa:joao";
	std::vector<std::string> strings = BPS::split(str, ':');
	for (size_t i = 0; i < strings.size(); i++) {
		std::cout << strings[i] + "\n";
	}
}

void section_tests() {
	BPS::Section section = BPS::Section("section");
	section.add(new BPS::Data("key", "value"));
	section.add(new BPS::Data("key2", "value2"));
	section.add(new BPS::Data("key3", "value3"));
	section.add(new BPS::Data("key4", "value4"));

	BPS::Data* dt1 = section.find("key5");
	if (dt1 != nullptr) {
		std::cout << dt1->getKey() + ":" + dt1->getValue() + "\n";
	}

	BPS::Data* dt2 = section.find("key");
	if (dt2 != nullptr) {
		std::cout << dt2->getKey() + ":" + dt2->getValue() + "\n";
	}

	std::cout << "\n";

	if (section.exists("key"))
		std::cout << "key? true\n";
	else
		std::cout << "key? false\n";

	if (section.exists("key5"))
		std::cout << "key5? true\n";
	else
		std::cout << "key5? false\n";

	std::cout << "\n";

	std::cout << section.getName() + "\n";
	for (BPS::Data* d : section.findAll()) {
		std::cout << d->getKey() + ":" + d->getValue() + "\n";
	}
}
